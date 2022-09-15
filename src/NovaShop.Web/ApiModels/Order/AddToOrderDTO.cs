namespace NovaShop.Web.ApiModels.Order;

public class AddToOrderDTO
{
    [Required]
    public int CatalogItemId { get; set; }

    [Required]
    public int Count { get; set; }
}