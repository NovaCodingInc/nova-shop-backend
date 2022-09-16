namespace NovaShop.ApplicationCore.OrderAggregate.Commands.UpdateOrderDetail;

public class UpdateOrderDetailCommand : IRequest<bool>
{
    public string CustomerId { get; set; }
    public int CatalogItemId { get; set; }
    public int Count { get; set; }

    public UpdateOrderDetailCommand(string customerId, int catalogItemId, int count)
    {
        CustomerId = customerId;
        CatalogItemId = catalogItemId;
        Count = count;
    }
}