namespace NovaShop.ApplicationCore.CatalogAggregate.Handlers;

public class UpdateQuantityInStockNotificationHandler : INotificationHandler<UpdateQuantityInStockEvent>
{
    public Task Handle(UpdateQuantityInStockEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send a mail or log something

        return Task.CompletedTask;
    }
}