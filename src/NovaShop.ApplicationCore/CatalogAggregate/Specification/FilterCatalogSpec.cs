namespace NovaShop.ApplicationCore.CatalogAggregate.Specification;

public class FilterCatalogSpec : Specification<CatalogItem>
{
    public FilterCatalogSpec(string? search = null,
        string? brand = null, string? category = null,
        int skip = 0, int take = 0,
        int orderBy = 0) : base()
    {
        if (take == 0)
            take = int.MaxValue;

        switch (orderBy)
        {
            case 0:
                Filter(search, brand, category, skip, take)
                    .OrderByDescending(i => i.CreateDate);
                break;
            case 1:
                Filter(search, brand, category, skip, take)
                    .OrderBy(i => i.CreateDate);
                break;
            case 2:
                Filter(search, brand, category, skip, take)
                    .OrderByDescending(i => i.Price);
                break;
            case 3:
                Filter(search, brand, category, skip, take)
                    .OrderBy(i => i.Price);
                break;
            default:
                Filter(search, brand, category, skip, take)
                    .OrderByDescending(i => i.CreateDate);
                break;
        }
    }

    private ISpecificationBuilder<CatalogItem> Filter(string? search = null,
        string? brand = null, string? category = null,
        int skip = 0, int take = 0)
    {
        return Query
            .Include(c => c.CatalogBrand)
            .Include(c => c.CatalogCategory)
            .Where(c =>
                (string.IsNullOrEmpty(brand) || c.CatalogBrand.Brand == brand) &&
                (string.IsNullOrEmpty(search) || EF.Functions.Like(c.Name, $"%{search}%")) &&
                (string.IsNullOrEmpty(category) || EF.Functions.Like(c.CatalogCategory.Category, $"%{category}%"))
            )
            .Skip(skip).Take(take);
    }
}