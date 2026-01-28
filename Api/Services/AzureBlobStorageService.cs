using Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Api;

public class AzureBlobStorageService : IStorageService
{
    private readonly BlobContainerClient _blobContainer;

    public AzureBlobStorageService(IConfiguration configuration, string containerName)
    {
        var connectionString = configuration.GetConnectionString("AzureBlobStorage");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new MissingFieldException($"{nameof(AzureBlobStorageService)} connection string is missing.");
        }

        _blobContainer = new BlobContainerClient(connectionString, containerName);
        _blobContainer.CreateIfNotExists();
        _blobContainer.SetAccessPolicy(PublicAccessType.Blob);
    }

    public async Task<string> UploadFileAsync(string blobName, Stream stream)
    {
        var blobClient = _blobContainer.GetBlobClient(blobName);
        await blobClient.UploadAsync(stream, overwrite: true);
        return blobClient.Uri.ToString();
    }

    public async Task<Stream> DownloadFileAsync(string blobName)
    {
        var blobClient = _blobContainer.GetBlobClient(blobName);
        var memoryStream = new MemoryStream();
        await blobClient.DownloadToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return memoryStream;
    }

    public async Task<bool> DeleteFileAsync(string blobName)
    {
        var blobClient = _blobContainer.GetBlobClient(blobName);
        var response = await blobClient.DeleteIfExistsAsync();
        return response.Value;
    }
}
