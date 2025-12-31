using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Identity.Application.Features.Login;

public record LoginCommand(string Username, string Password) : IRequest<Result>;
