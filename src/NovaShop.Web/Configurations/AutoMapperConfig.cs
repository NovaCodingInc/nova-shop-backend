using NovaShop.Web.Configuration.AutoMapper;

namespace NovaShop.Web.Configurations;

public static class AutoMapperConfig
{
    public static void AddAutoMapperProfile(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
    }
}