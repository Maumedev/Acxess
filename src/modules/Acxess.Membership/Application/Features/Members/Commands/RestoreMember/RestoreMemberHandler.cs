using Acxess.Membership.Domain.Abstractions;
using Acxess.Membership.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Membership.Application.Features.Members.Commands.RestoreMember;

public class RestoreMemberHandler(
    MembershipModuleContext context,
    IMembershipUnitOfWork unitOfWork) : IRequestHandler<RestoreMemberCommand,  Result<string>>  
{
    public async Task<Result<string>> Handle(RestoreMemberCommand request, CancellationToken ct)
    {
        var member = await context.Members.FindAsync([request.MemberId], ct);
        
        if (member is null) return Result<string>.Failure(Error.NotFound("Member.NotFound", "Socio no encontrado."));

        member.Restore();
        
        var result = await unitOfWork.SaveChangesAsync(ct);
        
        return result.IsSuccess 
            ? "Socio eliminado"
            : Result<string>.Failure(result.Error); 
    }
}