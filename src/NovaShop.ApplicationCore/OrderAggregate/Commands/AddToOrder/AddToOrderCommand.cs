namespace NovaShop.ApplicationCore.OrderAggregate.Commands.AddToOrder;

public class AddToOrderCommand : IRequest<AddToOrderCommandResponse>
{
    public string CustomerId { get; set; }
    public int CatalogItemId { get; set; }
    public int Count { get; set; }

    public AddToOrderCommand(string customerId, int catalogItemId, int count)
    {
        CustomerId = customerId;
        CatalogItemId = catalogItemId;
        Count = count;
    }
}