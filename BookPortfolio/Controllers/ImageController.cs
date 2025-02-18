using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

public class ImageController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly IHttpClientFactory _clientFactory;
    private const string ImageCacheFolder = "cache";

    public ImageController(IWebHostEnvironment env, IHttpClientFactory clientFactory)
    {
        _env = env;
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> GetCachedImage(string imageUrl)
    {
        var fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);
        var cachePath = Path.Combine(_env.WebRootPath, ImageCacheFolder, fileName);
        if (!System.IO.File.Exists(cachePath))
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(imageUrl);
            if (response.IsSuccessStatusCode)
            {
                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                Directory.CreateDirectory(Path.GetDirectoryName(cachePath));
                await System.IO.File.WriteAllBytesAsync(cachePath, imageBytes);
            }
            else
            {
                return NotFound("Image could not be downloaded.");
            }
        }
        var fileBytes = await System.IO.File.ReadAllBytesAsync(cachePath);
            Response.Headers["cache-control"] = "public, max-age=604800";
        return File(fileBytes, "image/jpeg");
    }
}