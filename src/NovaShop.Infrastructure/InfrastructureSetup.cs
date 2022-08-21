namespace NovaShop.Infrastructure;

public static class InfrastructureSetup
{
    public static void AddDatabase(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<NovaShopDbContext>(options => options.UseSqlServer(connectionString));
}