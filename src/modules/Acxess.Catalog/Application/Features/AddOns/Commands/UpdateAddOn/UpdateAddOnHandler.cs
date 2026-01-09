using Acxess.Catalog.Domain.Abstractions;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.AddOns.Commands.UpdateAddOn;

public class UpdateAddOnHandler(
    // IAddOnRepository addOnRepository,
    // ICatalogUnitOfWork unitOfWork
) : IRequestHandler<UpdateAddOnCommand, Result<string>>
{
    public Task<Result<string>> Handle(UpdateAddOnCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}