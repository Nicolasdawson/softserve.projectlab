using System.Net.Sockets;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Helpers;

public class FileStorage : IFileStorage
{
    private readonly string _connnectionString;

    public FileStorage(IConfiguration configuration)
    {
        _connnectionString = configuration.GetConnectionString("AzureStorage");
    }

    public async Task RemoveFileAsync(string path, string containerName)
    {
        var client = new BlobContainerClient(_connnectionString, containerName);
        await client.CreateIfNotExistsAsync();
        var fileName = Path.GetFileName(path);
        var blob = client.GetBlobClient(fileName);
        await blob.DeleteIfExistsAsync();
    }

    public async Task<string> SaveFileAsync(byte[] content, string extention, string containerName)
    {
        var client = new BlobContainerClient(_connnectionString, containerName);
        await client.CreateIfNotExistsAsync();
        client.SetAccessPolicy(PublicAccessType.Blob);
        var fileName = $"{Guid.NewGuid()}{extention}";
        var blob = client.GetBlobClient(fileName);

        using (var ms = new MemoryStream(content))
        {
            await blob.UploadAsync(ms);
        }
        return blob.Uri.ToString();
    }


    public async Task<string> SaveLocalFileAsync(IFormFile image)
    {
        var folder = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Products");

        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var filePath = Path.Combine(folder, fileName);        
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }
        /*        
        Console.WriteLine($"{Environment.CurrentDirectory}\\Images\\Products\\");
        Console.WriteLine(Directory.GetCurrentDirectory());
        */


        return fileName;
    }
}
