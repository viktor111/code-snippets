public class ClaimsBlobContainer : BlobStorageGenericService<ClaimsBlobContainer>
{
    private const string CONTAINER_NAME = "claims";
    
    public ClaimsBlobContainer(BlobServiceClient blobClient) :
        base(blobClient.GetBlobContainerClient(CONTAINER_NAME))
    {
    }
}