using Acxess.Membership.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Membership.Application.Features.Members.Queries.SearchEligibleMembers;

public class SearchEligibleMembersHandler(
    MembershipModuleContext context) : IRequestHandler<SearchEligibleMembersQuery, Result<List<EligibleMemberDto>>>
{
    public async Task<Result<List<EligibleMemberDto>>> Handle(SearchEligibleMembersQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Term) )
            return Result<List<EligibleMemberDto>>.Success([]);
        
        var today = DateTime.UtcNow.Date;
        
        var query = context.Members.AsNoTracking();
        
        var isNumeric = int.TryParse(request.Term, out var id);
        
        if (isNumeric) query = query.Where(m => m.IdMember == id);
        else 
        {
            query = query.Where(m =>
                m.FirstName.Contains(request.Term) ||
                m.LastName.Contains(request.Term));
        }
        
        var candidates = await query
            .Take(10)
            .Select(m => new 
            {
                m.IdMember,
                m.FirstName, 
                m.LastName,
                m.Phone,
                m.Email,
                ActiveSubscription = m.SubscriptionMemberships
                    .Where(sm => sm.Subscription.EndDate >= today && sm.Subscription.IsActive)
                    .Select(sm => new { sm.Subscription.EndDate, sm.Subscription.IdSellingPlan })
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);
        
        var result = candidates.Select(c =>
        {
            var isEligible = c.ActiveSubscription == null;
            
            var reason = isEligible 
                ? "Disponible" 
                : $"Tiene plan activo (Vence: {c.ActiveSubscription!.EndDate:dd/MM/yy})";

            return new EligibleMemberDto(
                c.IdMember, 
                c.FirstName, 
                c.LastName, 
                c.Phone, 
                c.Email, 
                isEligible, 
                reason);
        }).ToList();

        return result;
    }
}