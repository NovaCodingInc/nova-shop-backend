namespace NovaShop.Web.Controllers;

public class CatalogController : ApiBaseController
{
    #region constrcutor

    private readonly IRepository<CatalogItem> _catalogItemsRepository;
    private readonly IRepository<CatalogBrand> _catalogBrandRepository;
    private readonly IRepository<CatalogCategory> _catalogCategory;
    private readonly IMapper _mapper;
    private readonly CatalogSettings _catalogSettings;

    public CatalogController(IRepository<CatalogItem> catalogItemsRepository, IMapper mapper, IOptions<CatalogSettings> catalogSetting, IRepository<CatalogBrand> catalogBrandRepository, IRepository<CatalogCategory> catalogCategory)
    {
        _catalogItemsRepository = catalogItemsRepository;
        _mapper = mapper;
        _catalogBrandRepository = catalogBrandRepository;
        _catalogCategory = catalogCategory;
        _catalogSettings = catalogSetting.Value;
    }

    #endregion

    #region filter catalog

    [HttpGet]
    [ProducesResponseType(typeof(FilterProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FilterProductDTO>> GetCatalogItems([FromQuery] int pageId = 1, [FromQuery] string? search = null,
        [FromQuery] string? brand = null, [FromQuery] string? category = null, int take = 10,
        [FromQuery] FilterProductOrderBy orderBy = FilterProductOrderBy.CreateData_Des)
    {
        var filter = new FilterProductDTO
        {
            Search = search, Category = category, Brand = brand,
            OrderBy = orderBy,
            PageId = pageId, TakeEntity = take, HowManyShowPageAfterAndBefore = 3
        };

        var filterCatalogSpec =
            new FilterCatalogSpec(filter.Search, filter.Brand, filter.Category,
                (filter.PageId - 1) * filter.TakeEntity, filter.TakeEntity, (int)filter.OrderBy);

        var pager = Pager.Build(filter.PageId,
            await _catalogItemsRepository.CountAsync(filterCatalogSpec),
            filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

        var allEntities = await _catalogItemsRepository.ListAsync(filterCatalogSpec);
        FillProductPictureUris(allEntities);
        var allCatalogs = _mapper.Map<List<CatalogItemDTO>>(allEntities);

        return Ok(filter.SetProducts(allCatalogs).SetPaging(pager));
    }

    #endregion

    #region get catalog by id

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CatalogItemDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CatalogItemDTO>> GetCatalogItemById(int id)
    {
        var getCatalogItemSpec = new GetCatalogItemSpec(id);
        var catalogItem = await _catalogItemsRepository.SingleOrDefaultAsync(getCatalogItemSpec);

        if (catalogItem == null) return NotFound();
        catalogItem.UpdatePictureUri(_catalogSettings.CatalogPictureBaseUri);
        FillProductGalleryPictureUris(catalogItem.Galleries);

        var catalogItemDto = _mapper.Map<CatalogItemDTO>(catalogItem);

        return Ok(catalogItemDto);
    }

    #endregion

    #region get brands

    [HttpGet("brands")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<string>>> GetAllBrands()
    {
        var brands = await _catalogBrandRepository.ListAsync();
        return Ok(_mapper.Map<CatalogBrandDTO>(brands));
    }

    #endregion

    #region get categories

    [HttpGet("categories")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<string>>> GetAllCategory()
    {
        var categories = await _catalogCategory.ListAsync();
        return Ok(_mapper.Map<CatalogCategoryDTO>(categories));
    }

    #endregion

    #region utilities

    private void FillProductPictureUris(List<CatalogItem> items)
    {
        var baseUri = _catalogSettings.CatalogPictureBaseUri;
        foreach (var item in items)
        {
            item.UpdatePictureUri(baseUri);
        }
    }

    private void FillProductGalleryPictureUris(IReadOnlyCollection<CatalogGallery>? items)
    {
        if (items == null || !items.Any()) return;

        var baseUri = _catalogSettings.CatalogGalleryPictureBaseUri;
        foreach (var item in items)
        {
            item.UpdatePictureUri(baseUri);
        }
    }

    #endregion
}