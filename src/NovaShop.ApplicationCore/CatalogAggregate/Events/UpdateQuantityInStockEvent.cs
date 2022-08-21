namespace NovaShop.ApplicationCore.CatalogAggregate.Events;

public class UpdateQuantityInStockEvent : DomainEventBase
{
    public CatalogItem CatalogItem { get; set; }

    public int QuantityInStock { get; set; }

    public AddOrRemoveCatalogQuantityInStock AddOrRemove { get; set; }

    public UpdateQuantityInStockEvent(CatalogItem catalogItem, int quantityInStock, AddOrRemoveCatalogQuantityInStock addOrRemove)
    {
        CatalogItem = catalogItem;
        QuantityInStock = quantityInStock;
        AddOrRemove = addOrRemove;
    }
}