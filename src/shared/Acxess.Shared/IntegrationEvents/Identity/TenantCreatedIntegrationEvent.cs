using MediatR;

namespace Acxess.Shared.IntegrationEvents.Identity;

public record TenantCreatedIntegrationEvent(int TenantId) : INotification;
