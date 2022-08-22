namespace NovaShop.ApplicationCore.Services;

public class CatalogService : ICatalogService
{
    #region constrcutor

    private readonly IRepository<CatalogItem> _catalogItemsRepository;
    private readonly IMapper _mapper;

    public CatalogService(IRepository<CatalogItem> catalogItemsRepository, IMapper mapper)
    {
        _catalogItemsRepository = catalogItemsRepository;
        _mapper = mapper;
    }

    #endregion

    #region filter catalog

    public async Task<FilterProductViewModel> FilterProduct(FilterProductViewModel filter)
    {
        var filterCatalogSpec =
            new FilterCatalogSpec(filter.Search, filter.Brand, filter.Category, 
                (filter.PageId - 1) * filter.TakeEntity, filter.TakeEntity);

        var pager = Pager.Build(filter.PageId, 
            await _catalogItemsRepository.CountAsync(filterCatalogSpec),
            filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
        
        var allEntities = await _catalogItemsRepository.ListAsync(filterCatalogSpec);
        var allCatalogs = _mapper.Map<List<CatalogItemViewModel>>(allEntities);

        return filter.SetProducts(allCatalogs).SetPaging(pager);
    }

    #endregion
}