using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Membership.Application.Features.Members.Commands.UpdateMember;

public record UpdateMemberCommand(int Id, 
    string FirstName, 
    string LastName, 
    string? Phone, 
    string? Email
) : IRequest<Result<string>>;