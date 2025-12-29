using System;
using Acxess.Core.Modules.Identity.Entities;
using Acxess.Infrastructure.Persistence.Configurations.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Infrastructure.Persistence;

public class AcxessContext(DbContextOptions<AcxessContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Tenant> Tenants {get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

         

        builder.Entity<ApplicationUser>()
            .Property(u => u.UserNumber)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn() 
            .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore); 

       builder.Entity<ApplicationUser>()
            .HasIndex(u => u.UserNumber)
            .IsUnique();

        builder.ApplyConfiguration(new TenantConfiguration());
    }
}
