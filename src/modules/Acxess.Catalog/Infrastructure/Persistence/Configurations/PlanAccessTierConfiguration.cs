using System;
using Acxess.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Catalog.Infrastructure.Persistence.Configurations;

public class PlanAccessTierConfiguration : IEntityTypeConfiguration<PlanAccessTiers>
{
    public void Configure(EntityTypeBuilder<PlanAccessTiers> builder)
    {
         builder.ToTable("PlanAccessTiers");

        builder.HasKey(t => t.PlanAccessTierId);
        
        builder.Property(t => t.PlanAccessTierId)
               .UseIdentityColumn(); 

        builder.Property(t => t.AccessTierId)
               .IsRequired();

        builder.Property(t => t.SellingPlanId)
               .IsRequired();


    }
}
