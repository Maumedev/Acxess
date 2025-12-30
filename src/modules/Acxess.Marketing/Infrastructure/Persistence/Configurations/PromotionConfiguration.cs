using Acxess.Marketing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Marketing.Infrastructure.Persistence.Configurations;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.HasKey(x => x.IdPromotion);

        builder.Property(x => x.IdPromotion).UseIdentityColumn();

        builder.Property(x => x.IdTenant).IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DiscountAmount)
            .HasPrecision(10, 2);

        builder.Property(x => x.DiscountPercentage)
            .HasPrecision(5, 2);

        builder.Property(x => x.RequiresCoupon)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.MinItemsPurchase);

        builder.Property(x => x.AvailableFrom);

        builder.Property(x => x.AvailableTo);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.CreatedByUser).IsRequired();

        
    }
}
