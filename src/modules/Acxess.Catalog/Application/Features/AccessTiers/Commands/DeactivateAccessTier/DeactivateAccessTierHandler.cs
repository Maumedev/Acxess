using Acxess.Catalog.Domain.Abstractions;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.AccessTiers.Commands.DeactivateAccessTier;

public class DeactivateAccessTierHandler(
    IAccessTierRepository accessTierRepository,
    ICatalogUnitOfWork catalogUnitOfWork
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

        var resultUpdated = await catalogUnitOfWork.SaveChangesAsync(cancellationToken);

        if (resultUpdated.IsFailure)
        {
            return Result<string>.Failure("DeactivateFailed", "Failed to deactivate Access Tier.");
        }

        return Result<string>.Success("Access Tier deactivated successfully.");
    }
}