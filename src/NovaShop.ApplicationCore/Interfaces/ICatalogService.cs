namespace NovaShop.ApplicationCore.Interfaces;

public interface ICatalogService
{
    Task<FilterProductViewModel> FilterProduct(FilterProductViewModel filter);
}