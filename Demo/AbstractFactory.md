```mermaid
---
title: Abstract Factory
---
classDiagram
    StorageService <|-- AzureStorageService
    StorageServiceFactory <|-- AzureStorageServiceFactory
    StorageServiceFactory : +CreateStorageService()
    AzureStorageServiceFactory: +CreateStorageService()
    AzureStorageService <.. AzureStorageServiceFactory
    class StorageService{
        +Upload()
        +Delete()
    }
    class StorageServiceFactory{
        +CreateStorage()
    }
    class AzureStorageServiceFactory{
        +CreateStorage()
    }
```
