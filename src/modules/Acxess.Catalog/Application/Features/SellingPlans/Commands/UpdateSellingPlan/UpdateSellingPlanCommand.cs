using Acxess.Shared.Enums;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.SellingPlans.Commands.UpdateSellingPlan;

public record UpdateSellingPlanCommand(
    int IdSellingPlan,
    int TotalMembers,
    int Duration,
    DurationSubscriptionUnit DurationSubscriptionUnit,
    string Name,
    decimal Price,
    List<int> AccessTiersIds,
    bool IsActive
) : IRequest<Result<string>>;