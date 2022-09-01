namespace NovaShop.ApplicationCore.CustomerAggregate.Commands;

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand>
{
    private readonly IRepository<Customer> _customerRepository;

    public RegisterCustomerCommandHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.UserId);
        await _customerRepository.AddAsync(customer, cancellationToken);
        return Unit.Value;
    }
}