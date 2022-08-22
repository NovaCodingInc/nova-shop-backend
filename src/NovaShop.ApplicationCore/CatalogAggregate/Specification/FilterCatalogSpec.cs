namespace NovaShop.ApplicationCore.CatalogAggregate.Specification;

public class FilterCatalogSpec : Specification<CatalogItem>
{
    public FilterCatalogSpec(string? search = null,
        string? brand = null, string? category = null,
        int skip = 0, int take = 0,
        FilterProductOrderBy orderBy = FilterProductOrderBy.CreateData_Des) : base()
    {
        if (take == 0)
            take = int.MaxValue;

        switch (orderBy)
        {
            case FilterProductOrderBy.CreateData_Des:
                Filter(search, brand, category, skip, take).OrderByDescending(i => i.CreateDate);
                break;
            case FilterProductOrderBy.CreateData_Asc:
                Filter(search, brand, category, skip, take).OrderBy(i => i.CreateDate);
                break;
            case FilterProductOrderBy.Price_Des:
                Filter(search, brand, category, skip, take).OrderByDescending(i => i.Price);
                break;
            case FilterProductOrderBy.Price_Asc:
                Filter(search, brand, category, skip, take).OrderBy(i => i.Price);
                break;
        }
    }

    private ISpecificationBuilder<CatalogItem> Filter(string? search = null,
        string? brand = null, string? category = null,
        int skip = 0, int take = 0)
    {
        return Query.Include(c => c.CatalogBrand)
            .Where(c =>
                (string.IsNullOrEmpty(brand) || c.CatalogBrand.Brand == brand) &&
                (string.IsNullOrEmpty(search) || EF.Functions.Like(c.Name, $"%{search}%")) &&
                (string.IsNullOrEmpty(category) || EF.Functions.Like(c.Category, $"%{category}%"))
            )
            .Skip(skip).Take(take);
    }
}