using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace NovaShop.Web.Cdn.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("blazor-upload-picture-cors")]
public class UploadController : ControllerBase
{
    // TODO: Implement upload action for upload picture in pictures folder
}