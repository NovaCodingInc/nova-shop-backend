namespace NovaShop.ApplicationCore.OrderAggregate.Commands.AddToOrderCommand;

public class AddToOrderCommandHandler : IRequestHandler<AddToOrderCommand>
{
    #region constructor

    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<CatalogItem> _catalogItemRepository;

    public AddToOrderCommandHandler(IRepository<Order> orderRepository, 
        IRepository<CatalogItem> catalogItemRepository)
    {
        _orderRepository = orderRepository;
        _catalogItemRepository = catalogItemRepository;
    }

    #endregion

    public async Task<Unit> Handle(AddToOrderCommand request, CancellationToken cancellationToken)
    {
        var getCatalogItemSpec = new GetCatalogItemSpec(request.CatalogItemId);
        var catalogItem = await _catalogItemRepository.AnyAsync(getCatalogItemSpec, cancellationToken);

        if (catalogItem)
        {
            var getOpenOrderSpec = new GetOpenOrderSpec(request.CustomerId);

            var order = await _orderRepository.FirstOrDefaultAsync(getOpenOrderSpec, cancellationToken);
            if (order == null)
            {
                // new order
                // after finally calculate totalPrice
                order = new Order(request.CustomerId, 0, false);
                await _orderRepository.AddAsync(order, cancellationToken);
            }

            // after pay calculate product price
            order.AddOrderDetail(request.CatalogItemId, 0, request.Count);
            await _orderRepository.UpdateAsync(order, cancellationToken);
        }

        return Unit.Value;
    }
}