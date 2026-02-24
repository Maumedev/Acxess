using Acxess.Membership.Application.Features.Members.Queries.GetMember;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Membership.Application.Features.Members.Queries.GetMemberById;

public record GetMemberByIdQuery(int IdMember) :  IRequest<Result<MemberResponse>>;