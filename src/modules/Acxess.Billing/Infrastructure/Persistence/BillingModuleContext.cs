using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Acxess.Billing.Domain.Entities;

namespace Acxess.Billing.Infrastructure.Persistence;

public class BillingModuleContext(DbContextOptions<BillingModuleContext> options) : DbContext(options)
{
    public DbSet<MemberTransaction> MemberTransactions { get; set; }
    public DbSet<MemberTransactionDetail> MemberTransactionDetails { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Billing");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
