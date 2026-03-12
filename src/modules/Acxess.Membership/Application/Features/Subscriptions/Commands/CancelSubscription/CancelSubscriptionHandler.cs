using Acxess.Membership.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Membership.Application.Features.Subscriptions.Commands.CancelSubscription;

public class CancelSubscriptionHandler(
    MembershipModuleContext context) : IRequestHandler<CancelSubscriptionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
    {
        
        var memberId = await context.SubscriptionMembers
            .Where(sm => sm.IdSubscription == request.SubscriptionId)
            .Select(sm => sm.IdMember)
            .FirstOrDefaultAsync(cancellationToken);

        if (memberId == 0) 
            return Result<string>.Failure(Error.NotFound("Subscription.NotFound", "La suscripción no existe o no tiene socio asignado."));
        
        var activeSubscriptionsToCancel = await context.SubscriptionMembers
            .Include(sm => sm.Subscription)
            .Where(sm => sm.IdMember == memberId && sm.Subscription.IsActive)
            .Select(sm => sm.Subscription)
            .ToListAsync(cancellationToken);

        foreach (var sub in activeSubscriptionsToCancel)
        {
            sub.Cancel(request.Reason, request.UserId);
        }

        await context.SaveChangesAsync(cancellationToken);

        return  "Subscription cancelled.";
    }
}