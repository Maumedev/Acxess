using System;
using System.Reflection;
using Acxess.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Catalog.Infrastructure.Persistence;

public class CatalogModuleContext(DbContextOptions<CatalogModuleContext> options) : DbContext(options)
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
        
        // Multi-tenancy (Global Query Filter)
        // modelBuilder.Entity<AccessTier>().HasQueryFilter(a => a.TenantId == _currentTenantId);
    }
}
