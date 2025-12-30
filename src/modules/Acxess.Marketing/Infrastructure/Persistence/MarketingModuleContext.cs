using System.Reflection;
using Acxess.Marketing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Marketing.Infrastructure.Persistence;

public class MarketingModuleContext(DbContextOptions<MarketingModuleContext> options) : DbContext(options)
{
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<AppliedPromotion> AppliedPromotions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("Marketing");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
