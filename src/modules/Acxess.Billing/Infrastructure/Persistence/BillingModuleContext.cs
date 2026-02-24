using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Acxess.Billing.Domain.Entities;
using Acxess.Shared.Abstractions;

namespace Acxess.Billing.Infrastructure.Persistence;

public class BillingModuleContext(
    DbContextOptions<BillingModuleContext> options,
    ICurrentTenant currentTenant) : DbContext(options)
{
    public DbSet<MemberTransaction> MemberTransactions { get; set; }
    public DbSet<MemberTransactionDetail> MemberTransactionDetails { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Billing");

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
