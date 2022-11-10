[HttpGet("{userId}/{claimId}/{fileName}")]
public async Task<ActionResult> DownloadFile([FromRoute] string fileName)
{
    try
    {
        var file = await _documentsClaimsClient.GetClaimFileAsync(newGetClaimFileRequest
        {
            FileName = fileName
        });

        if (file.Response.IsError) throw new Exception(file.Response.Error);
        var bytes = file.File.Bytes.ToByteArray();
        return File(bytes, file.File.ContentType);
            
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}