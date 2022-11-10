public abstract class BlobStorageGenericService<T> : IBlobStorageService<T>
{
    private readonly BlobContainerClient _containerClient;
    public BlobStorageGenericService(BlobContainerClient containerClient)
    {
        _containerClient = containerClient; 
    }
    public virtual async Task<BlobInfo> GetBlob(string path)
    {
        var blobClient = _containerClient.GetBlobClient(path);
        
        var isFileExist = await blobClient.ExistsAsync();
        if (!isFileExist)
        {
            throw new BlobNotFoundException("File does not exist");
        }
        
        var blobDownloadResult = await blobClient.DownloadAsync();

        return new BlobInfo()
        {
           Stream = blobDownloadResult.Value.Content,
           ContentType = blobDownloadResult.Value.ContentType
        };
    }

    public virtual async Task<IEnumerable<string>> ListBlobsNames()
    {
        var items = new List<string>();

        await foreach (var blob in _containerClient.GetBlobsAsync())
        {
            items.Add(blob.Name);
        }

        return items;
    }

    public async Task<IEnumerable<string>> ListBlobsNames(string path)
    {
        var list = _containerClient.GetBlobsAsync(BlobTraits.All, BlobStates.All, path);
        var items = new List<string>();

        await foreach (var blob in list)
        {
            items.Add(blob.Name);
        }

        return items;
    }

    public virtual async Task<IEnumerable<string>> ListBlobsUris(string path)
    {
        var list = _containerClient.GetBlobsAsync(BlobTraits.All, BlobStates.All, path);
        var items = new List<string>();

        await foreach (var blob in list)
        {
            var blobClient = _containerClient.GetBlobClient($"{blob.Name}");
            items.Add(blobClient.Uri.ToString());
        }

        return items;
    }
    
    public virtual async Task UploadBlob(UploadBlobDto uploadBlobDto)
    {
        var blobPath = $"{uploadBlobDto.FilePath}/{uploadBlobDto.FileName}";
        var blobClient = _containerClient.GetBlobClient(blobPath);

        await blobClient.UploadAsync(uploadBlobDto.Stream, new BlobHttpHeaders()
        {
            ContentType = uploadBlobDto.ContentType
        });
    }

    public virtual async Task<bool> DeleteBlob(DeleteBlobDto deleteBlobDto)
    {
        var blobPath = $"{deleteBlobDto.FilePath}/{deleteBlobDto.FileName}";
        var blobClient = _containerClient.GetBlobClient(blobPath);

        return await blobClient.DeleteIfExistsAsync();
    }

    public async Task UploadManyBlobs(IEnumerable<UploadBlobDto> blobs)
    {
        foreach (var uploadBlobDto in blobs)
        {
            var blobPath = $"{uploadBlobDto.FilePath}/{uploadBlobDto.FileName}";
            var blobClient = _containerClient.GetBlobClient(blobPath);
            
            await blobClient.UploadAsync(uploadBlobDto.Stream, new BlobHttpHeaders()
            {
                ContentType = uploadBlobDto.ContentType
            });
        } 
    }
}