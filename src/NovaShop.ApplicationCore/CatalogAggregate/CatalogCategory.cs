namespace NovaShop.ApplicationCore.CatalogAggregate;

public class CatalogCategory : EntityBase, IAggregateRoot
{
    public string Category { get; private set; }

    public ICollection<CatalogItem> CatalogItems { get; set; } = null!;

    public CatalogCategory(string category)
    {
        Category = Guard.Against.NullOrEmpty(category);
    }

    public void UpdateBrand(string newCategory)
    {
        Category = Guard.Against.NullOrEmpty(newCategory);
    }
}