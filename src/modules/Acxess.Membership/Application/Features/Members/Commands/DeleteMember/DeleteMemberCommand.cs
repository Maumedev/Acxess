using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Membership.Application.Features.Members.Commands.DeleteMember;

public record DeleteMemberCommand(int MemberId, int UserId) : IRequest<Result<string>>;