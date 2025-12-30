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
        return services;
    }
}
