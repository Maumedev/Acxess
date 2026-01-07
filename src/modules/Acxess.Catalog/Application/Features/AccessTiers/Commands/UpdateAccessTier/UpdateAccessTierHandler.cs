using Acxess.Catalog.Domain.Abstractions;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.AccessTiers.Commands.UpdateAccessTier;

public class UpdateAccessTierHandler(
    IAccessTierRepository accessTierRepository,
    ICatalogUnitOfWork catalogUnitOfWork
) : IRequestHandler<UpdateAccessTierCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateAccessTierCommand request, CancellationToken cancellationToken)
    {
        var accessTier = await accessTierRepository.GetById(request.Id, cancellationToken);
        if (accessTier is null)
        {
            return Result<string>.Failure("NotFound", "Access Tier not found.");
        }

        accessTier.Update(request.Name, request.Description);
        accessTierRepository.Update(accessTier);
        
        var resultUpdated =await catalogUnitOfWork.SaveChangesAsync(cancellationToken);

        if (resultUpdated.IsFailure)
        {
            return Result<string>.Failure("UpdateFailed", "Failed to update Access Tier.");
        }

        return Result<string>.Success("Access Tier updated successfully.");
    }
}
