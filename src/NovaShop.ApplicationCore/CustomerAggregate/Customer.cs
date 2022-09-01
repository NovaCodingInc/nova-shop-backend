namespace NovaShop.ApplicationCore.CustomerAggregate;

public class Customer : EntityBase, IAggregateRoot
{
    public string UserId { get; private set; } = string.Empty;

    private Customer() { }

    public Customer(string userId)
    {
        Guard.Against.NullOrEmpty(userId, nameof(userId));
        UserId = userId;
    }
}