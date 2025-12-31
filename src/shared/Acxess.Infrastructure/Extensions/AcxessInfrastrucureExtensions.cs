using Acxess.Infrastructure.Services;
using Acxess.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Acxess.Infrastructure.Extensions;

public static class AcxessInfrastrucureExtensions
{
    public static IServiceCollection AddAcxessInfrastructure(
        this IServiceCollection services,
        params System.Reflection.Assembly[] moduleAssemblies)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentTenant, CurrentTenantService>();

        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssemblies(moduleAssemblies);
            cfg.RegisterServicesFromAssembly(typeof(AcxessInfrastrucureExtensions).Assembly);
            cfg.AddOpenBehavior(typeof(BehaviorsMediatR.TransactionalBehavior<,>));
        });

        return services;
    }
}
