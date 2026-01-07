using Acxess.Shared.ResultManager;
using MediatR;
namespace Acxess.Catalog.Application.Features.AccessTiers.Queries.GetAccessTiers;

public record GetAccessTiersQuery(string? Search, string? SortOrder, int Page, int PageSize)
: IRequest<Result<PaginatedResult<AccessTierDto>>> ; 


public record AccessTierDto
(
    int Id,
    string Name,
    string Description,
    bool IsActive
);