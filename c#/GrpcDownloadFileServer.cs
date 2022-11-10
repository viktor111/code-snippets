var bytesFromStream = await ByteString.FromStreamAsync(result.Stream);
var file = new File()
{
    Bytes = bytesFromStream,
    ContentType = result.ContentType,
    Name = request.FileName
};

return file;