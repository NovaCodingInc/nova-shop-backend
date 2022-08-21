namespace NovaShop.ApplicationCore.CatalogAggregate.Events;

public class NewGalleryAddedEvent : DomainEventBase
{
    public CatalogItem CatalogItem { get; set; }
    public CatalogGallery CatalogGallery { get; set; }

    public NewGalleryAddedEvent(CatalogItem catalogItem, CatalogGallery catalogGallery)
    {
        CatalogItem = catalogItem;
        CatalogGallery = catalogGallery;
    }
}