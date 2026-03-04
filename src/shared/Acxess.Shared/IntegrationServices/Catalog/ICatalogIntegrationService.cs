using Acxess.Shared.Enums;
using Acxess.Shared.ResultManager;

namespace Acxess.Shared.IntegrationServices.Catalog;

public interface ICatalogIntegrationService
{
    Task<PlanIntegrationDto?> GetPlanInfoAsync(int planId, CancellationToken ct = default);
    Task<Result<AddOnIntegrationDto>> GetAddOnPriceAsync(int addOnId, CancellationToken ct = default);
    Task<Result<List<string>>> GetAddOnNamesAsync(List<int> addOnIds, bool includesInscription, CancellationToken ct = default);
}

public record PlanIntegrationDto(int Id, string Name, decimal Price, int Duration, DurationSubscriptionUnit DurationUnit);

public record AddOnIntegrationDto(string Name, decimal Price);