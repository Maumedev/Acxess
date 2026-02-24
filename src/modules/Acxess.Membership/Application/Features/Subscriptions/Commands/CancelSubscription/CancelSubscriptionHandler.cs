using Acxess.Membership.Domain.Abstractions;
using Acxess.Membership.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Membership.Application.Features.Subscriptions.Commands.CancelSubscription;

public class CancelSubscriptionHandler(
    MembershipModuleContext context,
    IMembershipUnitOfWork unitOfWork) : IRequestHandler<CancelSubscriptionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await context.Subscriptions
            .FirstOrDefaultAsync(s => s.IdSubscription == request.SubscriptionId, cancellationToken);
        
        if (subscription is null)
            return Result<string>.Failure(Error.NotFound("Subscription.NotFound", "La suscripción no existe."));

        subscription.Cancel(request.Reason, request.UserId);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.IsSuccess
            ? "Subscription cancelled."
            : Result<string>.Failure(result.Error);
    }
}