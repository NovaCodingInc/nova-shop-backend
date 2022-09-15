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

    public Order(string customerId, decimal totalPrice, bool isPaid, DateTimeOffset? modifiedDate)
    {
        CustomerId = customerId;
        TotalPrice = totalPrice;
        IsPaid = isPaid;
    }

    public void AddOrderDetail(OrderDetail newDetail)
    {
        Guard.Against.Null(newDetail, nameof(newDetail));
        _orderDetails.Add(newDetail);

        // Register new domain event
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