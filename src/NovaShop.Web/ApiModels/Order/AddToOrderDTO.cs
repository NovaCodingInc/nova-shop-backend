namespace NovaShop.Web.ApiModels.Order;

public class AddToOrderDTO
{
    [Required]
    public int CatalogItemId { get; set; }

    [Required]
    public int Count { get; set; }
}

public class AddToOrderResponseDTO
{
    public AddToOrderResponseDTO(int count)
    {
        Count = count;
    }

    public int Count { get; set; }
}