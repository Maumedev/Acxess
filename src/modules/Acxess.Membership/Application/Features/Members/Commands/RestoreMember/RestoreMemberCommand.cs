using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Membership.Application.Features.Members.Commands.RestoreMember;

public record RestoreMemberCommand(int MemberId) : IRequest<Result<string>>;