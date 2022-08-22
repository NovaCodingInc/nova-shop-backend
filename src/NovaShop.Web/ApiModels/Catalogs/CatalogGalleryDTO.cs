namespace NovaShop.Web.ApiModels.Catalogs;

public class CatalogGalleryDTO
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public int DisplayPriority { get; set; }
    public string? PictureFileName { get; set; }
    public string? PictureUri { get; set; }
}