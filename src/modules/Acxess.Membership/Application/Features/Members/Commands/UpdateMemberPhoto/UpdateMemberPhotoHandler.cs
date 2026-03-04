using Acxess.Membership.Infrastructure.Persistence;
using Acxess.Shared.Abstractions;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Membership.Application.Features.Members.Commands.UpdateMemberPhoto;

public class UpdateMemberPhotoHandler(
    MembershipModuleContext context,
    IImageStorageService imageStorage) : IRequestHandler<UpdateMemberPhotoCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateMemberPhotoCommand request, CancellationToken cancellationToken)
    {
        var member = await context.Members.FirstOrDefaultAsync(m => m.IdMember == request.MemberId, cancellationToken);
        if (member == null) return Result<string>.Failure("Member.NotFound", "Socio no encontrado");

        if (!string.IsNullOrEmpty(member.PhotoUrl))
        {
            await imageStorage.DeleteImageAsync(member.PhotoUrl, cancellationToken);
        }

        var cleanName = $"{member.FirstName}-{member.LastName}".ToLower().Replace(" ", "-");
        var newPhotoUrl = await imageStorage.SaveImageAsync(request.PhotoBase64, cleanName, cancellationToken);
        
        member.UpdatePhoto(newPhotoUrl.Value);
        await context.SaveChangesAsync(cancellationToken);

        return newPhotoUrl.Value;
    }
}