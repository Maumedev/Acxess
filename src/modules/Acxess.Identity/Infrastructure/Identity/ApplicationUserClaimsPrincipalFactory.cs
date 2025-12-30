using System.Security.Claims;
using Acxess.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Acxess.Identity.Infrastructure.Identity;

public class ApplicationUserClaimsPrincipalFactory(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor) 
    : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>(userManager, roleManager, optionsAccessor)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        if (user.IdTenant.HasValue)
        {
            identity.AddClaim(new Claim("IdTenant", user.IdTenant.Value.ToString()));
        }

        identity.AddClaim(new Claim("UserNumber", user.UserNumber.ToString()));
        identity.AddClaim(new Claim("UserName", user.UserName ?? string.Empty));
        identity.AddClaim(new Claim("FullName", user.FullName ?? string.Empty));
        identity.AddClaim(new Claim("Email", user.Email ?? string.Empty));

        return identity;
    }
}
