using System;
using System.Reflection;
using Acxess.Identity.Domain.Entities;
using Acxess.Identity.Infrastructure.Persistence.Configurations;
using Acxess.Shared.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Identity.Infrastructure.Persistence;

public class IdentityModuleContext(
    DbContextOptions<IdentityModuleContext> options,
    ICurrentTenant currentTenant) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Tenant> Tenants => Set<Tenant>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Identity");
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new TenantConfiguration());
    }
    
    private void ApplyTenantFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            // IHasTenant (int IdTenant - Obligatorio)
            if (typeof(IHasTenant).IsAssignableFrom(clrType))
            {
                var method = this.GetType()
                    .GetMethod(nameof(ConfigureHasTenantFilter), BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.MakeGenericMethod(clrType);

                method?.Invoke(this, [modelBuilder]);
            }
            // IMayHaveTenant (int? IdTenant - Opcional)
            else if (typeof(IMayHaveTenant).IsAssignableFrom(clrType))
            {
                var method = this.GetType()
                    .GetMethod(nameof(ConfigureMayHaveTenantFilter), BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.MakeGenericMethod(clrType);

                method?.Invoke(this, [modelBuilder]);
            }
        }
    }
    
    private void ConfigureHasTenantFilter<T>(ModelBuilder builder) where T : class, IHasTenant
    {
        builder.Entity<T>().HasQueryFilter(e => e.IdTenant == currentTenant.Id);
    }
    
    private void ConfigureMayHaveTenantFilter<T>(ModelBuilder builder) where T : class, IMayHaveTenant
    {
        builder.Entity<T>().HasQueryFilter(e => e.IdTenant == currentTenant.Id || e.IdTenant == null);
    }
}
