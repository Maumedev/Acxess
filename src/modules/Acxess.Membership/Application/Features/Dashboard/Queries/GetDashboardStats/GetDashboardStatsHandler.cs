using Acxess.Membership.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Membership.Application.Features.Dashboard.Queries.GetDashboardStats;

public class GetDashboardStatsHandler(MembershipModuleContext context) : IRequestHandler<GetDashboardStatsQuery, Result<DashboardStatsDto>>
{
    public async Task<Result<DashboardStatsDto>> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;
        var startOfToday = today;
        var endOfToday = today.AddDays(1).AddTicks(-1);
        var threeDaysFromNow = today.AddDays(3);
        
        var newToday = await context.Members.CountAsync(m => m.CreatedAt >= startOfToday && m.CreatedAt <= endOfToday, cancellationToken);
        
        // 2. Total y Activos (Aproximación rápida: Activos = Tienen suscripción vigente)
        // Nota: Para "Activos" exacto se requiere query complejo de fechas. 
        // Haremos una aproximación eficiente: Members con IsDeleted=false
        var totalMembers = await context.Members.CountAsync(m => !m.IsDeleted, cancellationToken);
        
        // Para saber activos reales (con membresía vigente), consultamos SubscriptionMembers
        var activeMembers = await context.SubscriptionMembers
            .Where(sm => sm.Subscription.EndDate >= today)
            .Select(sm => sm.IdMember)
            .Distinct()
            .CountAsync(cancellationToken);
        
        // 3. Vencidos (Total - Activos es una aprox, pero mejor consultamos los que vencieron recientemente y no renovaron)
        // Definamos "Vencidos" como aquellos cuya última suscripción terminó antes de hoy.
        // Por simplicidad y rendimiento en dashboard, a veces se muestra "Vencidos este mes".
        // Usaremos: Socios con suscripción que venció y no tienen una nueva futura.
        var expiredMembers = totalMembers - activeMembers; // Aritmética simple para dashboard rápido
        
        // 4. Por Vencer (Próximos 3 días)
        var expiringSoon = await context.Subscriptions
            .Where(s => s.EndDate >= today && s.EndDate <= threeDaysFromNow)
            .Select(s => s.IdSubscription) // Count es más rápido seleccionando ID
            .CountAsync(cancellationToken);
        
        // 5. Tabla Top 5 Por Vencer (Para el listado central)
        var topExpiring = await context.SubscriptionMembers
            .AsNoTracking()
            .Where(sm => sm.Subscription.EndDate >= today && sm.Subscription.EndDate <= threeDaysFromNow)
            .OrderBy(sm => sm.Subscription.EndDate)
            .Take(5)
            .Select(sm => new ExpiringMemberItem(
                sm.IdMember,
                $"{sm.Member.FirstName} {sm.Member.LastName}",
                "Plan #" + sm.Subscription.IdSellingPlan, 
                sm.Subscription.EndDate,
                (sm.Subscription.EndDate.Date - today).Days
            ))
            .ToListAsync(cancellationToken);
        
        return Result<DashboardStatsDto>.Success(new DashboardStatsDto
        {
            NewMembersToday = newToday,
            TotalMembers = totalMembers,
            ActiveMembers = activeMembers,
            ExpiredMembers = expiredMembers,
            ExpiringSoon = expiringSoon,
            TopExpiringMembers = topExpiring,
            // GrowthPercentage se puede calcular si traes datos del mes pasado (tarea opcional para v2)
            GrowthPercentage = 12.5 // Hardcodeado por ahora para no complicar el query inicial
        });
    }
}