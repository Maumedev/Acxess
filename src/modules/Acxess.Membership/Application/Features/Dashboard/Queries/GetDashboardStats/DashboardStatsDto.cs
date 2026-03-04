namespace Acxess.Membership.Application.Features.Dashboard.Queries.GetDashboardStats;

public record
    DashboardStatsDto
{
    // Tarjeta 1: Nuevos Socios Hoy
    public int NewMembersToday { get; init; }
    public int NewMembersLastMonth { get; init; } 
    public double GrowthPercentage { get; init; }

    // Tarjeta 2: Total Socios
    public int TotalMembers { get; init; }
    public int ActiveMembers { get; init; }

    // Tarjeta 3: Vencidos
    public int ExpiredMembers { get; init; }

    // Tarjeta 4: Por Vencer (Próximos 3 días)
    public int ExpiringSoon { get; init; }
    
    // Tabla: Próximos Vencimientos (Top 5)
    public List<ExpiringMemberItem> TopExpiringMembers { get; init; } = [];
}

public record ExpiringMemberItem(int Id, string FullName, string PlanName, DateTime EndDate, int DaysLeft, string Initials, string? PhotoUrl);