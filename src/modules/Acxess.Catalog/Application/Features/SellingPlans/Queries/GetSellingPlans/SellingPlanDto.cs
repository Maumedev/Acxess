
using Acxess.Shared.Enums;

public record SellingPlanDto(
    int IdSellingPlan,
    string Name,
    int TotalMembers,
    int DurationInValue,
    DurationSubscriptionUnit DurationSubscriptionUnit, 
    decimal Price,
    bool IsActive,
    List<int> AccessTiersIds,
    string AccessTiers,
    string DurationDescription
);