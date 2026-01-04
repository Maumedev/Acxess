using Acxess.Identity.Domain.Absractions;
using Acxess.Identity.Domain.Entities;
using Acxess.Shared.Constants;
using Acxess.Shared.IntegrationEvents.Identity;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Acxess.Identity.Application.Features.Tenants.Commands.RegisterTenant;

public class RegisterTenantHandler(
    IIdentityUnitOfWork unitOfWork,
    ITenantRepository tenantRepository,
    UserManager< Domain.Entities.ApplicationUser> userManager,
    IMediator mediator
) : IRequestHandler<RegisterTenantCommand, Result>
{
    public async Task<Result> Handle(RegisterTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = Tenant.Create(request.NameTenant);

        tenantRepository.Add(tenant);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (result.IsFailure) return result;

        var idTenant = tenant.IdTenant;

        var userAdmin = Domain.Entities.ApplicationUser.Create(
            request.UsernameAdmin,
            request.EmailAdmin ?? string.Empty,
            request.FullNameAdmin,
            idTenant
        );

        var createUserResult = await userManager.CreateAsync(userAdmin, request.PasswordAdmin);

        if (!createUserResult.Succeeded)
        {
            var errors = string.Join("; ", createUserResult.Errors.Select(e => e.Description));
            return Result.Failure("ApplicationUser.NotCreated", $"Failed to create admin user: {errors}");
        }

        var addToRoleResult = await userManager.AddToRoleAsync(userAdmin, ApplicationRoles.SuperAdmin);

        if (!addToRoleResult.Succeeded)
        {
            var errors = string.Join("; ", addToRoleResult.Errors.Select(e => e.Description));
            return Result.Failure("ApplicationUser.RoleNotAssigned", $"Failed to assign role to admin user: {errors}");
        }

        await mediator.Publish(new TenantCreatedIntegrationEvent(idTenant), cancellationToken);

        return Result.Success();
    }
}
