using Acxess.Catalog.Domain.Enums;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.SellingPlans.Commands.NewSellingPlan;

public record NewSellingPlanCommand
(
    int TotalMembers,
    int Duration,
    DurationUnit DurationUnit,
    string Name,
    decimal Price,
    int CreatedBy,
    List<int> AccessTiersIds
) : IRequest<Result<string>>;
