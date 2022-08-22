using Microsoft.AspNetCore.Mvc;

namespace NovaShop.Cdn.Controllers;

public class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult Get() => Ok("cdn application for nova-shop");
}