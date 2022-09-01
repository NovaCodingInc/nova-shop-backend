namespace NovaShop.ApplicationCore.CustomerAggregate.Commands;

public class RegisterCustomerCommand : IRequest
{
    public string UserId { get; set; }

    public RegisterCustomerCommand(string userId)
    {
        UserId = userId;
    }
}