namespace NovaShop.Infrastructure.Data.Configurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.TotalPrice)
            .HasColumnType("decimal(18,4)");
    }
}