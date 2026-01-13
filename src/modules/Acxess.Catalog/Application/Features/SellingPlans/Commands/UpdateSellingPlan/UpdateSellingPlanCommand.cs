using Acxess.Catalog.Domain.Enums;
using Acxess.Shared.ResultManager;
using MediatR;

public record UpdateSellingPlanCommand(
    int IdSellingPlan,
    int TotalMembers,
    int Duration,
    DurationUnit DurationUnit,
    string Name,
    decimal Price,
    List<int> AccessTiersIds
) : IRequest<Result<string>>;