namespace NovaShop.Infrastructure.Data.Configurations;

public class CatalogGalleryEntityTypeConfiguration : IEntityTypeConfiguration<CatalogGallery>
{
    public void Configure(EntityTypeBuilder<CatalogGallery> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.PictureFileName).HasMaxLength(150);

        builder.HasOne(d => d.CatalogItem)
            .WithMany(p => p.Galleries)
            .HasForeignKey(d => d.CatalogItemId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CatalogGalleries_CatalogItems");
    }
}