using Acxess.Identity.Domain.Entities;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Acxess.Identity.Application.Features.Login;

public class LoginHandler(
    SignInManager<ApplicationUser> signInManager) : IRequestHandler<LoginCommand, Result>
{
    public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);
        
        if (result.Succeeded)
            return Result.Success();

        return Result.Failure("Login", "Invalid credentials");
    }
}