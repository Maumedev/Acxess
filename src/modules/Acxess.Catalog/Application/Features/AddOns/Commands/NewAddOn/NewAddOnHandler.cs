using Acxess.Catalog.Domain.Abstractions;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.AddOns.Commands.NewAddOn;

public class NewAddOnHandler(
    ICatalogUnitOfWork unitOfWork,
    IAddOnRepository addOnRepository
) : IRequestHandler<NewAddOnCommand, Result>
{
    public async Task<Result> Handle(NewAddOnCommand request, CancellationToken cancellationToken)
    {
        var addOn = Domain.Entities.AddOn.Create(
            request.TenantId,
            request.AddOnKey,
            request.Name,
            request.Price,
            request.ShowInCheckout
        );

        addOnRepository.Add(addOn);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
