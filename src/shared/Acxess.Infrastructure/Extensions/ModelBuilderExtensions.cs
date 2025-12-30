using System.Reflection;
using Acxess.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{

    public static void ApplyTenantFilters(this ModelBuilder modelBuilder, ICurrentTenant currentTenant)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IHasTenant).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(ModelBuilderExtensions)
                    .GetMethod(nameof(ConfigureTenantFilter), BindingFlags.NonPublic | BindingFlags.Static)
                    ?.MakeGenericMethod(entityType.ClrType);

                method?.Invoke(null, [modelBuilder, currentTenant]);
            }
        }
    }
    
     private static void ConfigureTenantFilter<T>(ModelBuilder builder, ICurrentTenant currentTenant) 
        where T : class, IHasTenant
    {
        builder.Entity<T>().HasQueryFilter(e => currentTenant.Id == null || e.IdTenant == currentTenant.Id);
    }
}
