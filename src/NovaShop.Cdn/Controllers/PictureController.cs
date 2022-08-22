using System.Net;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace NovaShop.Cdn.Controllers;

[ApiController]
public class PictureController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IDbConnection _db;

    public PictureController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        _webHostEnvironment = webHostEnvironment;
        _db = new SqlConnection(configuration.GetConnectionString("CatalogConnectionString"));
    }

    [HttpGet]
    [Route("catalog/{catalogItemId:int}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> GetCatalogImageAsync(int catalogItemId)
    {
        if (catalogItemId <= 0)
        {
            return BadRequest();
        }

        var pictureFileName = await _db.QuerySingleOrDefaultAsync<string>
            ("SELECT PictureFileName FROM CatalogItems WHERE Id = @Id", new { Id = catalogItemId });

        if (pictureFileName != null)
        {
            var webRoot = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(webRoot, pictureFileName);

            string imageFileExtension = Path.GetExtension(pictureFileName);
            string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

            var buffer = await System.IO.File.ReadAllBytesAsync(path);

            return File(buffer, mimetype);
        }

        return NotFound();
    }

    [HttpGet]
    [Route("catalog/gallery/{catalogGalleryId:int}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> GetCatalogGalleryImageAsync(int catalogGalleryId)
    {
        if (catalogGalleryId <= 0)
        {
            return BadRequest();
        }

        var pictureFileName = await _db.QuerySingleOrDefaultAsync<string>
            ("SELECT PictureFileName FROM CatalogGalleries WHERE Id = @Id", new { Id = catalogGalleryId });

        if (pictureFileName != null)
        {
            var webRoot = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(webRoot, "Gallery", pictureFileName);

            string imageFileExtension = Path.GetExtension(pictureFileName);
            string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

            var buffer = await System.IO.File.ReadAllBytesAsync(path);

            return File(buffer, mimetype);
        }

        return NotFound();
    }

    private string GetImageMimeTypeFromImageFileExtension(string extension)
    {
        string mimetype = extension switch
        {
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".bmp" => "image/bmp",
            ".tiff" => "image/tiff",
            ".wmf" => "image/wmf",
            ".jp2" => "image/jp2",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream",
        };
        return mimetype;
    }
}