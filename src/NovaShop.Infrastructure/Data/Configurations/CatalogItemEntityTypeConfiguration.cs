namespace NovaShop.Infrastructure.Data.Configurations;

public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Ignore(c => c.PictureUri);

        builder.Property(e => e.Category).HasMaxLength(250);

        builder.Property(e => e.Name).HasMaxLength(250);

        builder.Property(e => e.PictureFileName).HasMaxLength(150);

        builder.Property(e => e.Price).HasColumnType("decimal(18, 2)");

        builder.Property(e => e.Summary).HasMaxLength(400);

        builder.HasOne(d => d.CatalogBrand)
            .WithMany(p => p.CatalogItems)
            .HasForeignKey(d => d.CatalogBrandId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CatalogItems_CatalogBrands");
    }
}