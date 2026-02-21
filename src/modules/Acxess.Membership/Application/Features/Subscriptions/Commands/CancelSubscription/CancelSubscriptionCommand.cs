using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Membership.Application.Features.Subscriptions.Commands.CancelSubscription;

public record CancelSubscriptionCommand(int SubscriptionId, string Reason, int UserId) : IRequest<Result<string>>;