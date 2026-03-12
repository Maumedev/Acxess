using Acxess.Membership.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Membership.Application.Features.Members.Queries.GetMembers;

public class GetMembersHandler(
    MembershipModuleContext context) : IRequestHandler<GetMembersQuery, Result<MembersResponse>>
{
    public async Task<Result<MembersResponse>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = context.Members.AsNoTracking();

        // 1. Aplicar búsqueda por texto (Aplica a TODOS los estados)
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.Trim();
            if (int.TryParse(term, out var id))
            {
                baseQuery = baseQuery.Where(m => m.IdMember == id);
            }
            else
            {
                baseQuery = baseQuery.Where(m =>
                    m.FirstName.Contains(term) ||
                    m.LastName.Contains(term));
            }
        }

        var now = DateTime.Now;

        // 2. Calcular contadores GLOBALES (basados en la búsqueda, si la hay)
        var totalCount = await baseQuery.CountAsync(cancellationToken);
        
        var deletedCount = await baseQuery.CountAsync(m => m.IsDeleted, cancellationToken);
        
        // No eliminados
        var activeBase = baseQuery.Where(m => !m.IsDeleted);

        // Tienen suscripción activa que vence en el futuro o hoy
        var activeCount = await activeBase.CountAsync(m => 
            m.SubscriptionMemberships.Any(sm => sm.Subscription.EndDate >= now && sm.Subscription.IsActive), 
            cancellationToken);

        // Vencidos (No eliminados y que NO cumplen la condición de activos)
        // Por matemáticas: expired = (Total - eliminados) - activos
        var expiredCount = (totalCount - deletedCount) - activeCount;

        // 3. Aplicar Filtro de Estado para la LISTA a devolver
        var query = request.StatusFilter?.ToLower() switch
        {
            "active" => activeBase.Where(m => m.SubscriptionMemberships.Any(sm => sm.Subscription.EndDate >= now && sm.Subscription.IsActive)),
            "expired" => activeBase.Where(m => !m.SubscriptionMemberships.Any(sm => sm.Subscription.EndDate >= now && sm.Subscription.IsActive)),
            "deleted" => baseQuery.Where(m => m.IsDeleted),
            _ => activeBase // "all" o default: mostrar no eliminados
        };

        // 4. Obtener resultados de la lista filtrada
        var results = await query
            .OrderByDescending(m => m.UpdatedAt)
            // .Take(50) // ¡Te recomiendo MUCHO paginar esto o limitarlo en el futuro!
            .Select(m => new
            {
                m.IdMember,
                m.FirstName,
                m.LastName,
                m.Email,
                m.Phone,
                m.IsDeleted,
                m.PhotoUrl,
                HasActiveSubscription = m.SubscriptionMemberships
                    .Any(sm => sm.Subscription.EndDate >= now && sm.Subscription.IsActive)
            })
            .ToListAsync(cancellationToken);

        var memberItems = results.Select(m => new MemberItem(
            m.IdMember,
            $"{m.FirstName} {m.LastName}",
            GetInitials(m.FirstName, m.LastName),
            m.HasActiveSubscription,
            m.IsDeleted,
            m.Email ?? string.Empty,
            m.Phone ?? string.Empty,
            m.PhotoUrl
        )).ToList();

        return new MembersResponse(
            totalCount, 
            memberItems.Count, 
            memberItems,
            activeCount,
            expiredCount,
            deletedCount
        );
    }
    
    private static string GetInitials(string first, string last)
    {
        return $"{first.FirstOrDefault()}{last.FirstOrDefault()}".ToUpper();
    }
}