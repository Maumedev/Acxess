using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Identity.Application.Features.Tenants.Commands.RegisterTenant;

public class RegisterTenantHandler : IRequestHandler<RegisterTenantCommand, Result>
{
    public async Task<Result> Handle(RegisterTenantCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
