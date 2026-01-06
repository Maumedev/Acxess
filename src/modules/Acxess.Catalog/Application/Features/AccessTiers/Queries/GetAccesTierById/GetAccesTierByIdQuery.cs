using Acxess.Catalog.Application.Features.AccessTiers.Queries.GetAccessTiers;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.AccessTiers.Queries.GetAccesTierById;

public record GetAccesTierByIdQuery(int Id) : IRequest<Result<AccessTierDto>>;

