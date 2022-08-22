namespace NovaShop.Infrastructure.Data.Configurations;

public class CatalogCategoryEntityTypeConfiguration : IEntityTypeConfiguration<CatalogCategory>
{
    public void Configure(EntityTypeBuilder<CatalogCategory> builder)
    {
        builder.Property(e => e.Category)
            .IsRequired()
            .HasMaxLength(250);
    }
}