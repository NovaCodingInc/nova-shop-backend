namespace NovaShop.ApplicationCore.OrderAggregate.Commands.DeleteOrderDetail;

public class DeleteOrderDetailCommand : IRequest
{
    public string CustomerId { get; set; }
    public int CatalogItemId { get; set; }

    public DeleteOrderDetailCommand(string customerId, int catalogItemId)
    {
        CustomerId = customerId;
        CatalogItemId = catalogItemId;
    }
}