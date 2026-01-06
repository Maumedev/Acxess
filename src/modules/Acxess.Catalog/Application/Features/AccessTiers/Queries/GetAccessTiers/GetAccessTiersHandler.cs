using Acxess.Catalog.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Catalog.Application.Features.AccessTiers.Queries.GetAccessTiers;

public class GetAccessTiersHandler(
    CatalogModuleContext context
) : IRequestHandler<GetAccessTiersQuery, Result<PaginatedResult<AccessTierDto>>>
{
    public async Task<Result<PaginatedResult<AccessTierDto>>> Handle(GetAccessTiersQuery request, CancellationToken cancellationToken)
    {
        var query = context.AccessTiers.AsNoTracking();

        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(x => x.Name.Contains(request.Search));
        }

        query = request.SortOrder switch
        {
            "name_desc" => query.OrderByDescending(x => x.Name),
            _ => query.OrderBy(x => x.Name)
        };

        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new AccessTierDto(
                p.IdAccessTier, 
                p.Name, 
                p.Description ?? "", 
                p.IsActive
            )) 
            .ToListAsync(cancellationToken);

        return new PaginatedResult<AccessTierDto>(items, count, request.Page, request.PageSize);
    }
}
