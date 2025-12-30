using System.Reflection;
using Acxess.Infrastructure.Extensions;
using Acxess.Marketing.Domain.Entities;
using Acxess.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Marketing.Infrastructure.Persistence;

public class MarketingModuleContext(
    DbContextOptions<MarketingModuleContext> options,
    ICurrentTenant current) : DbContext(options)
{
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<AppliedPromotion> AppliedPromotions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("Marketing");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyTenantFilters(current);
    }
}
