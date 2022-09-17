namespace NovaShop.Infrastructure.Data.Configurations;

public class OrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,4)");
    }
}