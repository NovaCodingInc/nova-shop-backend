namespace NovaShop.ApplicationCore;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CatalogService>()
            .As<ICatalogService>()
            .InstancePerLifetimeScope();
    }
}