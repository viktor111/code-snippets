using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var root = builder.Environment.ContentRootPath;

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings.Clear();
provider.Mappings[".proto"] = "text/plain";

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(root, "Protos")),
    RequestPath = "/proto",
    ContentTypeProvider = provider
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(root, "Protos")),
    RequestPath = "/proto"
});

app.MapControllers();



app.Run();