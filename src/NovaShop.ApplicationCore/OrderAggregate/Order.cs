using NovaShop.ApplicationCore.CatalogAggregate;

namespace NovaShop.ApplicationCore.OrderAggregate;

public class Order : EntityBase, IAggregateRoot
{
    public string CustomerId { get; set; }
    public decimal TotalPrice { get; private set; }
    public bool IsPaid { get; private set; }
    public DateTimeOffset CreateDate { get; private set; } = DateTimeOffset.Now;
    public DateTimeOffset? ModifiedDate { get; set; }

    private readonly List<OrderDetail> _orderDetails = new List<OrderDetail>();

    public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

    public Order(string customerId, decimal totalPrice, bool isPaid)
    {
        CustomerId = customerId;
        TotalPrice = totalPrice;
        IsPaid = isPaid;
    }

    public void AddOrderDetail(int catalogItemId, decimal productPrice, int quantity = 1)
    {
        if (IsPaid) return;
        Guard.Against.NegativeOrZero(catalogItemId, nameof(catalogItemId));
        Guard.Against.Negative(productPrice, nameof(productPrice));
        Guard.Against.Negative(quantity, nameof(quantity));

        if (_orderDetails.All(i => i.CatalogItemId != catalogItemId))
        {
            _orderDetails.Add(new OrderDetail(Id, catalogItemId, quantity, productPrice));
            return;
        }

        var existingItem = _orderDetails.FirstOrDefault(i => i.OrderId == Id && i.CatalogItemId == catalogItemId);
        existingItem?.AddQuantity(quantity);

        // Register new domain event
    }

    public void DeleteOrderDetail(OrderDetail detail)
    {
        Guard.Against.Null(detail, nameof(detail));
        _orderDetails.Remove(detail);
    }

    public void SetTotalPrice()
    {
        if (!IsPaid) return;
        decimal total = _orderDetails.Sum(o => (o.ProductPrice * o.Count));
        TotalPrice = total;
    }

    public void FinallyOrder()
    {
        IsPaid = true;

        // Register new domain event
    }

    public void UpdateModifiedDate()
    {
        ModifiedDate = DateTimeOffset.Now;
    }
}