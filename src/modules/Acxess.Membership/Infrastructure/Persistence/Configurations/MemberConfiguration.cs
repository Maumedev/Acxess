using System;
using Acxess.Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Membership.Infrastructure.Persistence.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(t => t.IdMember);
        
        builder.Property(t => t.IdMember)
            .UseIdentityColumn(); 

        builder.Property(t => t.IdTenant)
            .IsRequired();

        builder.Property(t => t.FirtsName)
        .HasMaxLength(80)
        .IsRequired();
      
        builder.Property(t => t.LastName)
        .HasMaxLength(150)
        .IsRequired();
        
        builder.Property(t => t.Email)
        .HasMaxLength(80);
        
        builder.Property(t => t.Phone)
        .HasMaxLength(13);


        builder.Property(t => t.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(rt => rt.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(t => t.CreatedByUser)
            .IsRequired();

        builder.HasIndex(t => t.IdTenant);
        builder.HasIndex(t => t.IdMember);
            
    }
}
