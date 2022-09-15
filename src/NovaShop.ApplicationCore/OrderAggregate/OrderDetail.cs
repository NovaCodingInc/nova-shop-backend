namespace NovaShop.ApplicationCore.OrderAggregate;

public class OrderDetail : EntityBase
{
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public int Count { get; private set; }
    public decimal ProductPrice { get; private set; }

    public OrderDetail(int orderId, int productId, int count, decimal productPrice)
    {
        OrderId = orderId;
        ProductId = productId;
        Count = count;
        ProductPrice = productPrice;
    }

    public void UpdateOrder(int orderId)
    {
        OrderId = Guard.Against.NegativeOrZero(orderId, nameof(orderId));
    }

    public void UpdateProduct(int productId)
    {
        ProductId = Guard.Against.NegativeOrZero(productId, nameof(productId));
    }

    public void UpdateCount(int count)
    {
        Count = Guard.Against.NegativeOrZero(count, nameof(count));
    }

    public void UpdateProductPrice(int productPrice)
    {
        ProductPrice = Guard.Against.Negative(productPrice);
    }
}