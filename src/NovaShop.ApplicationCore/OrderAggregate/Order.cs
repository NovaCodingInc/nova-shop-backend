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

    public OrderDetail? AddOrderDetail(int catalogItemId, decimal productPrice, int quantity = 1)
    {
        if (IsPaid) return null;
        Guard.Against.NegativeOrZero(catalogItemId, nameof(catalogItemId));
        Guard.Against.Negative(productPrice, nameof(productPrice));
        Guard.Against.Negative(quantity, nameof(quantity));

        if (_orderDetails.All(i => i.CatalogItemId != catalogItemId))
        {
            var newOrderDetail = new OrderDetail(Id, catalogItemId, quantity, productPrice);
            _orderDetails.Add(newOrderDetail);
            return newOrderDetail;
        }

        var existingItem = _orderDetails.SingleOrDefault(i => i.OrderId == Id && i.CatalogItemId == catalogItemId);
        existingItem?.AddQuantity(quantity);
        return existingItem;
    }

    public void DeleteOrderDetail(OrderDetail detail)
    {
        Guard.Against.Null(detail, nameof(detail));
        _orderDetails.Remove(detail);
    }

    public void SetTotalPrice()
    {
        if (!IsPaid) return;
        decimal total = _orderDetails.Sum(o => (o.Price * o.Count));
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