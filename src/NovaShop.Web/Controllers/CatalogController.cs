namespace NovaShop.Web.Controllers;

public class CatalogController : ApiBaseController
{
    #region constrcutor

    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    #endregion

    #region filter catalog

    [HttpGet]
    [ProducesResponseType(typeof(FilterProductViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FilterProductViewModel>> GetCatalogItems([FromQuery] int pageId = 1, [FromQuery] string? search = null,
        [FromQuery] string? brand = null, [FromQuery] string? category = null,
        [FromQuery] FilterProductOrderBy orderBy = FilterProductOrderBy.CreateData_Des)
    {
        var filter = new FilterProductViewModel
        {
            Search = search,
            Category = category,
            Brand = brand,
            OrderBy = orderBy,
            PageId = pageId,
            TakeEntity = 3,
            HowManyShowPageAfterAndBefore = 3
        };
        return Ok(await _catalogService.FilterProduct(filter));
    }

    #endregion
}