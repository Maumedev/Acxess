
using Acxess.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Catalog.Infrastructure.Persistence.Configurations;

public class AccessTierConfiguration : IEntityTypeConfiguration<AccessTier>
{
    public void Configure(EntityTypeBuilder<AccessTier> builder)
    {
        builder.ToTable("AccessTiers");

        builder.HasKey(t => t.IdAccessTier);
        
        builder.Property(t => t.IdAccessTier)
               .UseIdentityColumn(); 

        builder.Property(t => t.Name)
               .HasMaxLength(60)
               .IsRequired();

        builder.Property(t => t.Description)
               .HasMaxLength(150);

        builder.Property(t => t.IdTenant)
               .IsRequired();

        builder.Property(t => t.IsActive)
               .IsRequired()
               .HasDefaultValue(true);

        builder.HasIndex(t => t.IdTenant);
    }
}
