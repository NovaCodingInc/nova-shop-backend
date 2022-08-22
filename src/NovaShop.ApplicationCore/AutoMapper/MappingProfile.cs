namespace NovaShop.ApplicationCore.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItem, CatalogItemViewModel>();
    }
}