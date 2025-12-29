using System;
using Acxess.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Catalog.Infrastructure.Persistence.Configurations;

public class AddOnConfiguration : IEntityTypeConfiguration<AddOn>
{
    public void Configure(EntityTypeBuilder<AddOn> builder)
    {
        builder.ToTable("AddOns");

        builder.HasKey(t => t.AddOnId);
        
        builder.Property(t => t.AddOnId)
               .UseIdentityColumn(); 

        builder.Property(t => t.TenantId)
               .IsRequired();

        builder.HasIndex(t => t.TenantId);

        builder.HasIndex(t => new { t.TenantId, t.AddOnKey })
               .IsUnique();

        builder.Property(t => t.AddOnKey)
               .HasMaxLength(10)
               .IsRequired(); 

        builder.Property(t => t.Name)
               .HasMaxLength(50)
               .IsRequired(); 

        builder.Property(t => t.Price)
               .HasPrecision(10, 2)
               .IsRequired();
    }
}
