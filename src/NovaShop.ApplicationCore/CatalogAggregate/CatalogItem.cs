namespace NovaShop.ApplicationCore.CatalogAggregate;

public class CatalogItem : EntityBase, IAggregateRoot
{
    public int CatalogBrandId { get; private set; }
    public string Name { get; private set; }
    public string Category { get; private set; }
    public string Summary { get; private set; }
    public string Description { get; private set; }
    public string PictureFileName { get; private set; }
    public string PictureUri { get; private set; } = string.Empty;
    public int QuantityInStock { get; private set; }
    public decimal Price { get; private set; }
    public DateTimeOffset CreateDate { get; private set; } = DateTimeOffset.Now;
    public DateTimeOffset? ModifiedDate { get; set; }

    public CatalogBrand CatalogBrand { get; set; } = null!;

    private readonly List<CatalogGallery> _galleries = new List<CatalogGallery>();
    public IReadOnlyCollection<CatalogGallery> Galleries => _galleries.AsReadOnly();

    public CatalogItem(int catalogBrandId, string name, string category,
        string summary, string description, string pictureFileName,
        int quantityInStock, decimal price)
    {
        CatalogBrandId = catalogBrandId;
        Name = name;
        Category = category;
        Summary = summary;
        Description = description;
        PictureFileName = pictureFileName;
        QuantityInStock = quantityInStock;
        Price = price;
    }

    public void AddGallery(CatalogGallery newGallery)
    {
        Guard.Against.Null(newGallery, nameof(newGallery));
        _galleries.Add(newGallery);

        var newGalleryAddedEvent = new NewGalleryAddedEvent(this, newGallery);
        RegisterDomainEvent(newGalleryAddedEvent);
    }

    public void UpdateBrand(int newBrandId)
    {
        CatalogBrandId = Guard.Against.NegativeOrZero(newBrandId);
    }

    public void UpdateName(string name)
    {
        Name = Guard.Against.NullOrEmpty(name);
    }

    public void UpdateCategory(string category)
    {
        Category = Guard.Against.NullOrEmpty(category);
    }

    public void UpdateSummary(string summary)
    {
        Summary = Guard.Against.NullOrEmpty(summary);
    }

    public void UpdateDescription(string description)
    {
        Description = Guard.Against.NullOrEmpty(description);
    }

    public void UpdatePictureFileName(string pictureFileName)
    {
        PictureFileName = Guard.Against.NullOrEmpty(pictureFileName);
    }

    public void UpdatePictureUri(string pictureUri, string pictureBaseUrl)
    {
        Guard.Against.NullOrEmpty(pictureUri);
        Guard.Against.NullOrEmpty(pictureBaseUrl);
        PictureUri = pictureBaseUrl.Replace("[0]", this.Id.ToString());
    }

    public void UpdatePrice(int price)
    {
        Price = Guard.Against.NegativeOrZero(price);
    }

    public void AddOrRemoveQuantityInStock(int quantityInStock, 
        AddOrRemoveCatalogQuantityInStock addOrRemove)
    {
        Guard.Against.NegativeOrZero(quantityInStock);

        switch (addOrRemove)
        {
            case AddOrRemoveCatalogQuantityInStock.Add:
                // Add here
                this.QuantityInStock += quantityInStock;
                break;
            case AddOrRemoveCatalogQuantityInStock.Remove:
            {
                // Remove here
                int removed = Math.Min(quantityInStock, this.QuantityInStock);
                this.QuantityInStock -= removed;
                break;
            }
        }

        var updateQuantityInStockEvent = new UpdateQuantityInStockEvent(this, quantityInStock, addOrRemove);
        RegisterDomainEvent(updateQuantityInStockEvent);
    }

    public void UpdateModifiedDate()
    {
        ModifiedDate = DateTimeOffset.Now;
    }
}