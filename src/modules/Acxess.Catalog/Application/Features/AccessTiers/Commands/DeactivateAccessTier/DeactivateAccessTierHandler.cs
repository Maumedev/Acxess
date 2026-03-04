using Acxess.Catalog.Domain.Abstractions;
using Acxess.Catalog.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.AccessTiers.Commands.DeactivateAccessTier;

public class DeactivateAccessTierHandler(
    IAccessTierRepository accessTierRepository,
    CatalogModuleContext context
) : IRequestHandler<DeactivateAccessTierCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeactivateAccessTierCommand request, CancellationToken cancellationToken)
    {
        var accessTier = await accessTierRepository.GetById(request.Id, cancellationToken);
        if (accessTier is null)
        {
            return Result<string>.Failure("NotFound", "Access Tier not found.");
        }

        if (accessTier.IsActive)
            accessTier.Deactivate();
        else
            accessTier.Activate();

        accessTierRepository.Update(accessTier);

        await context.SaveChangesAsync(cancellationToken);


        return "Access Tier deactivated successfully.";
    }
}