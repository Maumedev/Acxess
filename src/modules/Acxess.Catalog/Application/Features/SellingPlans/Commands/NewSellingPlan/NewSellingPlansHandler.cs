using Acxess.Catalog.Domain.Abstractions;
using Acxess.Catalog.Domain.Entities;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.SellingPlans.Commands.NewSellingPlan;

public class NewSellingPlansHandler(
    ISellingPlanRepository sellingPlanRepository,
    ICatalogUnitOfWork catalogUnitOfWork
) : IRequestHandler<NewSellingPlanCommand, Result>
{
    public async Task<Result> Handle(NewSellingPlanCommand request, CancellationToken cancellationToken)
    {
        var sellingPlan = SellingPlan.Create(
            request.TenantId,
            request.Name,
            request.TotalMembers,
            request.Duration,
            request.DurationUnit,
            request.Price,
            request.CreatedBy
        );

        sellingPlanRepository.Add(sellingPlan);

        var resultSave = await catalogUnitOfWork.SaveChangesAsync(cancellationToken);   

        if (resultSave.IsFailure)
        {
            return Result.Failure(resultSave.Error);
        }

        return Result.Success();
        
    }
}
