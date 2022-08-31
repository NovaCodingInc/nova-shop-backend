namespace NovaShop.Web.ApiModels.Catalogs;

public class FilterProductDTO : BasePaging
{
    public string? Search { get; set; }

    public string? Brand { get; set; }

    public string? Category { get; set; }

    public FilterProductOrderBy OrderBy { get; set; }

    public List<CatalogItemDTO>? Products { get; set; }

    #region methods

    public FilterProductDTO SetProducts(List<CatalogItemDTO> products)
    {
        Products = products;
        return this;
    }

    public FilterProductDTO SetPaging(BasePaging paging)
    {
        PageId = paging.PageId;
        AllEntitiesCount = paging.AllEntitiesCount;
        StartPage = paging.StartPage;
        EndPage = paging.EndPage;
        HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
        Take = paging.Take;
        Skip = paging.Skip;
        PageCount = paging.PageCount;
        return this;
    }

    #endregion
}

public enum FilterProductOrderBy
{
    [Display(Name = "جدید&zwnj;ترین")]
    CreateData_Des = 0,
    [Display(Name = "قدیمی&zwnj;ترین")]
    CreateData_Asc = 1,
    [Display(Name = "گران&zwnj;ترین")]
    Price_Des = 2,
    [Display(Name = "ارزان&zwnj;ترین")]
    Price_Asc = 3
}