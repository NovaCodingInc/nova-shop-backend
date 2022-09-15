namespace NovaShop.ApplicationCore.OrderAggregate.Specification;

public class GetOpenOrderSpec : Specification<Order>
{
    public GetOpenOrderSpec(string customerId)
    {
        Query.Where(o => o.CustomerId == customerId && !o.IsPaid);
    }
}