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

        builder.HasKey(t => t.IdSellingPlan);
        
        builder.Property(t => t.IdSellingPlan)
               .UseIdentityColumn(); 

        builder.Property(t => t.IdTenant)
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


        builder.HasIndex(t => t.IdSellingPlan);
        builder.HasIndex(t => t.IdTenant);
        builder.HasIndex(t => t.CreatedByUser);
    }
}
