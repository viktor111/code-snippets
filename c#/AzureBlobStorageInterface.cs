public interface IBlobStorageService<T>
{
   Task<BlobInfo> GetBlob(string path);

   Task<IEnumerable<string>> ListBlobsNames();
   
   Task<IEnumerable<string>> ListBlobsNames(string path);

   Task<IEnumerable<string>> ListBlobsUris(string path);

   Task UploadBlob(UploadBlobDto uploadBlobDto);

   Task<bool> DeleteBlob(DeleteBlobDto deleteBlobDto);

   Task UploadManyBlobs(IEnumerable<UploadBlobDto> blobs);
}