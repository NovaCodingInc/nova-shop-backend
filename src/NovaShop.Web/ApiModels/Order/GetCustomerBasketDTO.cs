namespace NovaShop.Web.ApiModels.Order;

public class GetCustomerBasketDTO
{
    public List<BasketItemDTO>? Items { get; set; }
}

public class BasketItemDTO
{
    public int CatalogItemId { get; set; }
    public string? CatalogItemName { get; set; }
    public string? PictureUri { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}