using Acxess.Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Billing.Infrastructure.Persistence.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethods");

        builder.HasKey(pm => pm.IdPaymentMethod);

        builder.Property(pm => pm.IdPaymentMethod)
            .UseIdentityColumn();

        builder.Property(pm => pm.IdTenant);

        builder.Property(pm => pm.Method)
            .IsRequired()
            .HasMaxLength(100);
    }
}
