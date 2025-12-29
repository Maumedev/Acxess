using System;
using Acxess.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Catalog.Infrastructure.Persistence.Configurations;

public class SellingPlanConfiguration : IEntityTypeConfiguration<SellingPlan>
{
    public void Configure(EntityTypeBuilder<SellingPlan> builder)
    {
        builder.ToTable("SellingPlans");

        builder.HasKey(t => t.SellingPlanId);
        
        builder.Property(t => t.SellingPlanId)
               .UseIdentityColumn(); 

        builder.Property(t => t.TenantId)
               .IsRequired();


        builder.Property(n=> n.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(n=> n.TotalMembers)
            .IsRequired();

        
        builder.Property(n=> n.DurationInValue)
            .IsRequired()
            .HasColumnType("tinyint");

        builder.Property(e => e.DurationUnit)
            .HasConversion<int>();

        builder.Property(t => t.Price)
               .HasPrecision(10, 2)
               .IsRequired();

        builder.Property(t => t.IsActive)
               .IsRequired()
               .HasDefaultValue(true);
        
        builder.Property(rt => rt.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(t => t.CreatedByUser)
            .IsRequired();


        builder.HasIndex(t => t.SellingPlanId);
        builder.HasIndex(t => t.TenantId);
        builder.HasIndex(t => t.CreatedByUser);
    }
}
