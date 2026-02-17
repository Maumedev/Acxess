using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Membership.Application.Features.Members.Queries.GetRenewalMemberContext;

public record GetRenewalMemberContextQuery
(int MemberId) : IRequest<Result<RenewalContextDto>>;