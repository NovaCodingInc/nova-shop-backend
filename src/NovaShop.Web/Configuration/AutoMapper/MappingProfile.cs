namespace NovaShop.Web.Configuration.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItem, CatalogItemDTO>()
            .ForMember(dest => dest.Category,
                opts =>
                    opts.MapFrom(src =>
                    src.CatalogCategory.Category))
            .ForMember(dest => dest.Brand,
                opts =>
                    opts.MapFrom(src =>
                        src.CatalogBrand.Brand));

        CreateMap<CatalogGallery, CatalogGalleryDTO>();
        CreateMap<CatalogBrand, CatalogBrandDTO>();
        CreateMap<CatalogCategory, CatalogCategoryDTO>();
    }
}