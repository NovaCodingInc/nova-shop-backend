namespace NovaShop.ApplicationCore.Exceptions;

public class BasketItemNotFoundException : OrderDomainException
{
    public BasketItemNotFoundException(int basketId) :
        base($"No basket item found for basketId: {basketId}")
    {
    }
}