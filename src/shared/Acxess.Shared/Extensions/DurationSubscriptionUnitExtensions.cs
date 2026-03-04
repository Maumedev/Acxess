using Acxess.Shared.Enums;

namespace Acxess.Shared.Enums;

public static class DurationSubscriptionUnitExtensions
{
    public static string ToFriendlyName(this DurationSubscriptionUnit subscriptionUnit, int value)
    {
        return subscriptionUnit switch
        {
            DurationSubscriptionUnit.Days => value == 1 ? "Día" : "Días",
            DurationSubscriptionUnit.Months => value == 1 ? "Mes" : "Meses",
            DurationSubscriptionUnit.Years => value == 1 ? "Año" : "Años",
            _ => subscriptionUnit.ToString()
        };
    }
}