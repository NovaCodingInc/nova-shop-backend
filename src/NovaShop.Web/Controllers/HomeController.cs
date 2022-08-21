namespace NovaShop.Web.Controllers;

public class HomeController : ControllerBase
{
    #region constrcutor

    private readonly IWebHostEnvironment _webHostEnvironment;

    public HomeController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    #endregion

    [HttpGet("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Get() =>
        _webHostEnvironment.IsDevelopment() ?
            Redirect("/swagger") :
            NotFound();
}