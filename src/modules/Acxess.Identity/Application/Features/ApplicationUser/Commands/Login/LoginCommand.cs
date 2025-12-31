using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Identity.Application.Features.ApplicationUser.Commands.Login;

public record LoginCommand(string Username, string Password) : IRequest<Result>;
