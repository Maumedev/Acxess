using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acxess.Web.Pages.Catalog.AddOns;
public class CreateModel : PageModel
{
    public IActionResult OnGet()
    {
        if (Request.Headers.ContainsKey("HX-Request") || Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Partial("_Create", this);
        }
        
        return Page();
    }
}
