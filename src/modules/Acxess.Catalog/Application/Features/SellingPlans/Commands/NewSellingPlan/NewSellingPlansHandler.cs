using Acxess.Catalog.Domain.Abstractions;
using Acxess.Catalog.Domain.Entities;
using Acxess.Catalog.Infrastructure.Persistence;
using Acxess.Shared.Abstractions;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.SellingPlans.Commands.NewSellingPlan;

public class NewSellingPlansHandler(
    ISellingPlanRepository sellingPlanRepository,
    CatalogModuleContext context,
    ICurrentTenant currentTenant
) : IRequestHandler<NewSellingPlanCommand, Result<string>>
{
    
    public async Task<Result<string>> Handle(NewSellingPlanCommand request, CancellationToken cancellationToken)
    {
       if (!currentTenant.IsAvailable)
            return Result<string>.Failure("TenantId.NotAvailable","Tenant information is not available.");

       var sellingPlan = SellingPlan.Create(
           currentTenant.Id ?? 0,
           request.Name,
           request.TotalMembers,
           request.Duration,
           request.DurationSubscriptionUnit,
           request.Price,
           request.CreatedBy
       );

       if (request.AccessTiersIds.Count != 0)
       {
           foreach (var tierId in request.AccessTiersIds)
           {
               sellingPlan.AddAccessTier(tierId);
           }
       }

       sellingPlanRepository.Add(sellingPlan);

       var resultSave = await context.SaveChangesAsync(cancellationToken);   

       return "Plan guardado correctamente";
    }
}
