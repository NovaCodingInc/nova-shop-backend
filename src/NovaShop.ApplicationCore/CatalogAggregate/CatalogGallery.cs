namespace NovaShop.ApplicationCore.CatalogAggregate;

public class CatalogGallery : EntityBase
{
    public int CatalogItemId { get; private set; }
    public int DisplayPriority { get; private set; }
    public string PictureFileName { get; set; } = string.Empty;
    public string PictureUri { get; set; } = string.Empty;

    public CatalogItem CatalogItem { get; set; } = null!;

    public CatalogGallery(int catalogItemId, int displayPriority)
    {
        CatalogItemId = catalogItemId;
        DisplayPriority = displayPriority;
    }

    public void UpdatePictureUri(string? baseUri)
    {
        Guard.Against.NullOrEmpty(baseUri);
        PictureUri = baseUri.Replace("[0]", this.Id.ToString());
    }
}