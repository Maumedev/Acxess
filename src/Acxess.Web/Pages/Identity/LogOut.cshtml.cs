using Acxess.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acxess.Web.Pages.Identity;
public class LogOutModel(SignInManager<ApplicationUser> signInManager) : PageModel
{
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await signInManager.SignOutAsync(); 
        return RedirectToPage("/Identity/Login");
    }
}
