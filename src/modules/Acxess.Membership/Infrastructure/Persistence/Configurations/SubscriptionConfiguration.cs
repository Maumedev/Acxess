using Acxess.Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Membership.Infrastructure.Persistence.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
         builder.ToTable("Subscriptions");

        builder.HasKey(t => t.IdSubscription);
        
        builder.Property(t => t.IdSubscription)
            .UseIdentityColumn(); 

        builder.Property(t => t.IdTenant)
            .IsRequired();

        builder.Property(t => t.IdMemberOwner)
            .IsRequired();

        builder.Property(t => t.IdSellingPlan)
            .IsRequired();

        builder.Property(t => t.IsAcive)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(rt => rt.StartDate)
            .IsRequired();

        builder.Property(rt => rt.EndDate)
            .IsRequired();

        builder.Property(rt => rt.PriceSnapshot)
            .HasPrecision(10,2)
            .IsRequired();

        builder.Property(rt => rt.Notes)
            .HasMaxLength(250);

        builder.Property(rt => rt.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(t => t.CreatedByUser)
            .IsRequired();

        builder.HasIndex(t => t.IdTenant);
    }
}
