using Acxess.Membership.Domain.Abstractions;
using Acxess.Membership.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Membership.Application.Features.Members.Commands.UpdateMember;

public class UpdateMemberHandler(
    MembershipModuleContext context,
    IMembershipUnitOfWork unitOfWork): IRequestHandler<UpdateMemberCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await context.Members
            .FirstOrDefaultAsync(m => m.IdMember == request.Id, cancellationToken);

        if (member is null)
            return Result<string>.Failure(Error.NotFound("Member.NotFound", "Socio no encontrado"));
        
        member.UpdateInformation(request.FirstName, request.LastName, request.Phone, request.Email);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.IsSuccess 
            ? "Información actualizada"
            : Result<string>.Failure(result.Error);
    }
}