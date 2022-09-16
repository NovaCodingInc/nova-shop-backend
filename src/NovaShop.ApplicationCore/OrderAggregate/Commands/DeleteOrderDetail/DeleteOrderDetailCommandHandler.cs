namespace NovaShop.ApplicationCore.OrderAggregate.Commands.DeleteOrderDetail;

public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand>
{
    #region constructor

    private readonly ILogger<DeleteOrderDetailCommandHandler> _logger;
    private readonly IRepository<Order> _orderRepository;

    public DeleteOrderDetailCommandHandler(ILogger<DeleteOrderDetailCommandHandler> logger,
        IRepository<Order> orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    #endregion

    public async Task<Unit> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var getOpenOrderSpec = new GetOpenOrderSpec(request.CustomerId);
        var openOrder = await _orderRepository.FirstOrDefaultAsync(getOpenOrderSpec, cancellationToken);
        if (openOrder != null)
        {
            var orderDetail = openOrder.OrderDetails.FirstOrDefault(detail => detail.OrderId == openOrder.Id && detail.CatalogItemId == request.CatalogItemId);
            if (orderDetail != null)
            {
                openOrder.DeleteOrderDetail(orderDetail);
                await _orderRepository.UpdateAsync(openOrder ,cancellationToken);
                _logger.LogInformation("Catalog Item {itemId} In Basket {orderId} Successfully Deleted", orderDetail.CatalogItemId, openOrder.Id);
            }
        }

        return Unit.Value;
    }
}