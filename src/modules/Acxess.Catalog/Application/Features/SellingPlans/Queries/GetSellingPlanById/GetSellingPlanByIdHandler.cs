using System;
using Acxess.Catalog.Infrastructure.Persistence;
using Acxess.Shared.Enums;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Catalog.Application.Features.SellingPlans.Queries.GetSellingPlanById;

public class GetSellingPlanByIdHandler(
    CatalogModuleContext context
) : IRequestHandler<GetSellingPlanByIdQuery, Result<SellingPlanDto>>
{
    public async Task<Result<SellingPlanDto>> Handle(GetSellingPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var query = context.SellingPlans.AsNoTracking()
            .Where(p => p.IdSellingPlan == request.IdSellingPlan);


        var item = await query
            .Select(p => new SellingPlanDto(
                p.IdSellingPlan,
                p.Name,
                p.TotalMembers,
                p.DurationInValue,
                p.DurationUnit,
                p.Price,
                p.IsActive,
                p.AccessTiers.Select(link => link.IdAccessTier).ToList(),
                string.Join(", ", p.AccessTiers.Select(link => link.AccessTier.Name)),
                $"{p.DurationInValue} {p.DurationUnit.ToFriendlyName(p.DurationInValue)}"
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return item ?? Result<SellingPlanDto>.Failure("NOT FOUND", "No se encontro el plan de venta");
    }
}
