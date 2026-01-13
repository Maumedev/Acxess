using System;
using Acxess.Catalog.Domain.Abstractions;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.SellingPlans.Commands.UpdateSellingPlan;

public class UpdateSellingPlanHandler(
    ISellingPlanRepository sellingPlanRepository,
    ICatalogUnitOfWork unitOfWork
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
            request.DurationUnit,
            request.Price
        );

        sellingPlan.SyncAccessTiers(request.AccessTiersIds ?? new List<int>());

        var resultSave = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (resultSave.IsFailure) return Result<string>.Failure(resultSave.Error);

        return "Plan actualizado correctamente";
    }
}
