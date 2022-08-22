namespace NovaShop.ApplicationCore.ViewModels.Catalogs;

public class FilterProductViewModel : BasePaging
{
    public string? Search { get; set; }
    public string? Brand { get; set; }
    public string? Category { get; set; }
    public FilterProductOrderBy OrderBy { get; set; }

    public List<CatalogItemViewModel>? Products { get; set; }

    #region methods

    public FilterProductViewModel SetProducts(List<CatalogItemViewModel> products)
    {
        Products = products;
        return this;
    }

    public FilterProductViewModel SetPaging(BasePaging paging)
    {
        PageId = paging.PageId;
        AllEntitiesCount = paging.AllEntitiesCount;
        StartPage = paging.StartPage;
        EndPage = paging.EndPage;
        HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
        TakeEntity = paging.TakeEntity;
        SkipEntity = paging.SkipEntity;
        PageCount = paging.PageCount;
        return this;
    }

    #endregion
}

public enum FilterProductOrderBy
{
    [Display(Name = "جدید&zwnj;ترین")]
    CreateData_Des,
    [Display(Name = "قدیمی&zwnj;ترین")]
    CreateData_Asc,
    [Display(Name = "گران&zwnj;ترین")]
    Price_Des,
    [Display(Name = "ارزان&zwnj;ترین")]
    Price_Asc
}