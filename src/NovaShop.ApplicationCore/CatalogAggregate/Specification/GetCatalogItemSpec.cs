namespace NovaShop.ApplicationCore.CatalogAggregate.Specification;

public class GetCatalogItemSpec : Specification<CatalogItem>, ISingleResultSpecification<CatalogItem>
{
    public GetCatalogItemSpec(int itemId)
    {
        Query
            .Include(c => c.CatalogCategory)
            .Include(c => c.CatalogBrand)
            .Include(c => c.Galleries)
            .Where(c => c.Id == itemId);
    }
}