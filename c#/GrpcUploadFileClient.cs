public async Task<ActionResult> UploadOne(IFormFile formFile)
{
    try
    {
        var bytes = await ByteString.FromStreamAsync(formFile.OpenReadStream());
        
        var request = _documentsClaimsClient.UploadOne(new FileUploadRequest
        {
            File = new Euroins.Documents.File()
            {
                Bytes = bytes,
                Name = formFile.FileName,
                ContentType = formFile.ContentType
                }
            });

        if (request.IsUploaded) return Ok();
        
        return BadRequest("Error processing file");
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}