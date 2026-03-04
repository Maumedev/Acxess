using System;
using Acxess.Catalog.Domain.Abstractions;
using Acxess.Catalog.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.SellingPlans.Commands.UpdateSellingPlan;

public class UpdateSellingPlanHandler(
    ISellingPlanRepository sellingPlanRepository,
    CatalogModuleContext context
) : IRequestHandler<UpdateSellingPlanCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateSellingPlanCommand request, CancellationToken cancellationToken)
    {
        var sellingPlan = await sellingPlanRepository.GetByIdWithTiersAsync(request.IdSellingPlan, cancellationToken);

        if (sellingPlan is null)
            return Result<string>.Failure("NotFound", "El plan no existe.");

        sellingPlan.Update(
            request.Name, 
            request.TotalMembers,
            request.Duration,
            request.DurationSubscriptionUnit,
            request.Price,
            request.IsActive
        );

        sellingPlan.SyncAccessTiers(request.AccessTiersIds ?? []);

        await context.SaveChangesAsync(cancellationToken);

        return "Plan actualizado correctamente";
    }
}
