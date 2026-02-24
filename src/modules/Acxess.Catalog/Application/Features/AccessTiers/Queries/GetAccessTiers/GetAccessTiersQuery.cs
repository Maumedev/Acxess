using Acxess.Shared.ResultManager;
using MediatR;
namespace Acxess.Catalog.Application.Features.AccessTiers.Queries.GetAccessTiers;

public record GetAccessTiersQuery(bool IncludesInactives)
: IRequest<Result<List<AccessTierDto>>>; 


public record AccessTierDto
(
    int IdAccessTier,
    string Name,
    string Description,
    bool IsActive
);