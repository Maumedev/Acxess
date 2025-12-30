using System;
using Acxess.Marketing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Marketing.Infrastructure.Persistence.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Coupon> builder)
    {
        builder.HasKey(x => x.IdCoupon);

        builder.Property(x => x.IdCoupon).UseIdentityColumn();

        builder.Property(x => x.IdTenant).IsRequired();

        builder.Property(x => x.IdMember).IsRequired();

        builder.Property(x => x.IdPromotion).IsRequired();

        builder.Property(x => x.IsRedeemed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.ExpiresOn);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.CreatedByUser).IsRequired();


    }
}
