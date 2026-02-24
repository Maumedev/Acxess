using Acxess.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Catalog.Infrastructure.Persistence.Configurations;

public class PlanAccessTierConfiguration : IEntityTypeConfiguration<PlanAccessTiers>
{
    public void Configure(EntityTypeBuilder<PlanAccessTiers> builder)
    {
         builder.ToTable("PlanAccessTiers");

        builder.HasKey(t => t.IdPlanAccessTier);
        
        builder.Property(t => t.IdPlanAccessTier)
               .UseIdentityColumn(); 

       //  builder.Property(t => t.IdAccessTier)
       //         .IsRequired();

       builder.HasOne(link => link.AccessTier)
               .WithMany()
               .HasForeignKey(link => link.IdAccessTier);

        builder.HasOne(link => link.SellingPlan)
               .WithMany(plan => plan.AccessTiers)
               .HasForeignKey(link => link.IdSellingPlan);


       //  builder.Property(t => t.IdSellingPlan)
       //         .IsRequired();


    }
}
