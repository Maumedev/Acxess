using Acxess.Catalog.Domain.Abstractions;
using Acxess.Catalog.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.AddOns.Commands.UpdateAddOn;

public class UpdateAddOnHandler(
    IAddOnRepository addOnRepository,
    CatalogModuleContext context
) : IRequestHandler<UpdateAddOnCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateAddOnCommand request, CancellationToken cancellationToken)
    {
        var addOn = await addOnRepository.GetById(request.IdAddOn, cancellationToken);

        if (addOn is null)
        {
            return Result<string>.Failure("NotFound", "Access Tier not found.");
        }

        addOn.Update(
            request.AddOnKey,
            request.Name,
            request.Price,
            request.ShowInCheckout,
            request.IsVisit,
            request.IsActive);

        await context.SaveChangesAsync(cancellationToken);

        return Result<string>.Success("Cambios guardados correctamente.");
        
    }
}