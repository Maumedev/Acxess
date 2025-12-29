using System.Reflection;
using Acxess.Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Membership.Infrastructure.Persistence;

public class MembershipModuleContext(DbContextOptions<MembershipModuleContext> options) : DbContext(options)
{
    public DbSet<Member> Members { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionMembers> SubscriptionMembers { get; set; }
    public DbSet<SubscriptionAddOns> SubscriptionAddOns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Membership");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
