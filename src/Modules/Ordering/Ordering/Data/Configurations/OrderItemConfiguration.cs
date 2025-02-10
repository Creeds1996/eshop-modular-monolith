namespace Ordering.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ProductId)
            .IsRequired();

        builder.Property(e => e.Quantity)
            .IsRequired();

        builder.Property(e => e.Price)
            .IsRequired();
    }
}