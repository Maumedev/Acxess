using System;
using Acxess.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acxess.Identity.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        
        builder.Property(u => u.UserNumber)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.HasAlternateKey(u => u.UserNumber);

    }
}