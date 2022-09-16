namespace NovaShop.ApplicationCore.OrderAggregate.Queries.GetCustomerBasket;

public class GetCustomerBasketQuery : IRequest<Order>
{
    public string CustomerId { get; set; }

    public GetCustomerBasketQuery(string customerId)
    {
        CustomerId = customerId;
    }
}