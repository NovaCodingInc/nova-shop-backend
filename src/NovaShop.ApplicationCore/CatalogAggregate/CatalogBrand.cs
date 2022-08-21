namespace NovaShop.ApplicationCore.CatalogAggregate;

public class CatalogBrand : EntityBase, IAggregateRoot
{
    public string Brand { get; private set; }

    public ICollection<CatalogItem> CatalogItems { get; set; } = null!;

    public CatalogBrand(string brand)
    {
        Brand = Guard.Against.NullOrEmpty(brand);
    }

    public void UpdateBrand(string newBrand)
    {
        Brand = Guard.Against.NullOrEmpty(newBrand);
    }
}