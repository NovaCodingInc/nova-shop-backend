namespace NovaShop.ApplicationCore.ViewModels.Catalogs;

public class CatalogItemViewModel
{
    public int Id { get; set; }
    public string? Brand { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? PictureFileName { get; set; }
    public string? PictureUri { get; set; }
    public int QuantityInStock { get; set; }
    public decimal Price { get; set; }

    public List<CatalogGalleryViewModel>? Galleries { get; set; }
}