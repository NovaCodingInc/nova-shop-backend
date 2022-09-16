namespace NovaShop.ApplicationCore.OrderAggregate.Queries.GetCustomerBasket;

public class GetCustomerBasketQueryHandler : IRequestHandler<GetCustomerBasketQuery, Order>
{
    private readonly ILogger<GetCustomerBasketQueryHandler> _logger;
    private readonly IRepository<Order> _orderRepository;

    public GetCustomerBasketQueryHandler(ILogger<GetCustomerBasketQueryHandler> logger, 
        IRepository<Order> orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<Order> Handle(GetCustomerBasketQuery request, CancellationToken cancellationToken)
    {
        var getOpenOrderSpec = new GetOpenOrderSpec(request.CustomerId);
        var order = await _orderRepository.FirstOrDefaultAsync(getOpenOrderSpec, cancellationToken);
        return order ?? new Order(request.CustomerId, 0, false);
    }
}