using MediatR;

namespace Acxess.Shared.IntegrationEvents.Membership;

public record MemberCreatedIntegrationEvent(int IdMember) : INotification;