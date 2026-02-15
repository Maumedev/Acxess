using System.Reflection;
using Acxess.Catalog.Domain.Entities;
using Acxess.Infrastructure.Extensions;
using Acxess.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Catalog.Infrastructure.Persistence;

public class CatalogModuleContext(
    DbContextOptions<CatalogModuleContext> options,
    ICurrentTenant currentTenant) : DbContext(options)
{

    public DbSet<AccessTier> AccessTiers { get; set; }
    public DbSet<SellingPlan> SellingPlans { get; set; }
    public DbSet<AddOn> AddOns { get; set; }
    public DbSet<PlanAccessTiers> PlanAccessTiers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Catalog");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        ApplyTenantFilters(modelBuilder); 
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
