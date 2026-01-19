namespace Demo;

public interface StorageService
{
    void Upload(string file);
    bool Delete(string file);
}

public interface StorageServiceFactory
{
    StorageService CreateStorage();
}

public class AzureStorageService : StorageService
{
    public void Upload(string file)
    {
        // upload to Azure Blob Storage
    }

    public bool Delete(string file)
    {
        // delete file on Azure Blob Storage
        return true;
    }
}

public class AzureStorageServiceFactory : StorageServiceFactory
{
    public StorageService CreateStorage()
    {
        return new AzureStorageService();
    }
}

public class Program
{
    public static void Main(String[] args)
    {
        var factory = new AzureStorageServiceFactory();
        var storage = factory.CreateStorage();
        storage.Upload("test.md");
        storage.Delete("test.md");
    }
}