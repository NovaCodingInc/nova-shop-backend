namespace NovaShop.ApplicationCore.OrderAggregate.Commands.UpdateOrderDetail;

public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, bool>
{
    #region constrcutor

    private readonly ILogger<UpdateOrderDetailCommandHandler> _logger;
    private readonly IRepository<Order> _orderRepository;

    public UpdateOrderDetailCommandHandler(ILogger<UpdateOrderDetailCommandHandler> logger,
        IRepository<Order> orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    #endregion

    public async Task<bool> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var getOpenOrderSpec = new GetOpenOrderSpec(request.CustomerId);
        var openOrder = await _orderRepository.FirstOrDefaultAsync(getOpenOrderSpec, cancellationToken);
        if (openOrder != null)
        {
            var orderDetail = openOrder.OrderDetails.FirstOrDefault(detail => detail.OrderId == openOrder.Id && detail.CatalogItemId == request.CatalogItemId);
            if (orderDetail != null)
            {
                orderDetail.UpdateCount(request.Count);
                await _orderRepository.UpdateAsync(openOrder, cancellationToken);
                return true;
            }
        }

        return false;
    }
}