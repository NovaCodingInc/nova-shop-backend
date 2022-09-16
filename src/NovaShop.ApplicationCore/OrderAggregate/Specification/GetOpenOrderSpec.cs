namespace NovaShop.ApplicationCore.OrderAggregate.Specification;

public sealed class GetOpenOrderSpec : Specification<Order>
{
    public GetOpenOrderSpec(string customerId)
    {
        Query
            .Include(o => o.OrderDetails)
            .ThenInclude(o => o.CatalogItem)
            .Where(o => o.CustomerId == customerId && !o.IsPaid);
    }
}