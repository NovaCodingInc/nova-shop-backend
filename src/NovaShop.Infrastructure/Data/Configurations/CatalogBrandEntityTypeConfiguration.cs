namespace NovaShop.Infrastructure.Data.Configurations;

public class CatalogBrandEntityTypeConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.Property(e => e.Brand)
            .IsRequired()
            .HasMaxLength(150);
    }
}