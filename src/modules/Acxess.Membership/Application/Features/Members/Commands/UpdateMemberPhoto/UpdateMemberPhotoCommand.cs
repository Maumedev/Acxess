using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Membership.Application.Features.Members.Commands.UpdateMemberPhoto;

public record UpdateMemberPhotoCommand(int MemberId, string PhotoBase64) : IRequest<Result<string>>;