using Acxess.Infrastructure.Services;
using Acxess.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Acxess.Infrastructure.Extensions;

public static class AcxessInfrastrucureExtensions
{
    public static IServiceCollection AddAcxessInfrastructure(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentTenant, CurrentTenantService>();

        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(typeof(AcxessInfrastrucureExtensions).Assembly);
            cfg.AddBehavior(typeof(BehaviorsMediatR.TransactionalBehavior<,>));
        });

        return services;
    }
}
