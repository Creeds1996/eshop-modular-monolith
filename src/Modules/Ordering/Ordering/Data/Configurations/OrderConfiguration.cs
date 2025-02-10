namespace Ordering.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CustomerId);

        builder.HasIndex(e => e.OrderName)
            .IsUnique();

        builder.Property(e => e.OrderName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(e => e.Items)
            .WithOne()
            .HasForeignKey(si => si.OrderId);

        builder.ComplexProperty(
            o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(e => e.Email)
                    .HasMaxLength(50);

                addressBuilder.Property(e => e.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(e => e.Country)
                    .HasMaxLength(50);

                addressBuilder.Property(e => e.State)
                    .HasMaxLength(50);

                addressBuilder.Property(e => e.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
            });
        
        builder.ComplexProperty(
            o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(e => e.Email)
                    .HasMaxLength(50);

                addressBuilder.Property(e => e.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(e => e.Country)
                    .HasMaxLength(50);

                addressBuilder.Property(e => e.State)
                    .HasMaxLength(50);

                addressBuilder.Property(e => e.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
            });

        builder.ComplexProperty(
            o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName)
                    .HasMaxLength(50);

                paymentBuilder.Property(e => e.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();

                paymentBuilder.Property(e => e.Expiration)
                    .HasMaxLength(10);

                paymentBuilder.Property(e => e.Cvv)
                    .HasMaxLength(3);

                paymentBuilder.Property(e => e.PaymentMethod);
            });
    }
}