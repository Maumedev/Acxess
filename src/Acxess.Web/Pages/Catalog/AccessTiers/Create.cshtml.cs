using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Acxess.Catalog.Application.Features.AccessTiers.Commands.AddAccessTier;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acxess.Web.Pages.Catalog.AccessTiers;

public class CreateSellingPlanInput
{
    [Required(ErrorMessage = "El nombre es obligatorio. ")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
public class CreateModel(IMediator mediator) : PageModel
{

    [BindProperty]
    public CreateSellingPlanInput Input { get; set; } = new();

    public IActionResult OnGet()
    {
        if (Request.Headers.ContainsKey("HX-Request") || Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Partial("_Create", this);
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) 
        {
            return Partial("_Create", this);
        }

        var addCommand = new AddAccessTierCommand(Input.Name, Input.Description);   
        var result = await mediator.Send(addCommand);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description);
            return Partial("_Create", this);
        }

        var triggers = new { 
            closeSlide = true,      
            refreshTable = true ,
            notify = new { 
                type = "success", 
                message = result.Value 
            }
        };
        
        Response.Headers.Append("HX-Trigger", JsonSerializer.Serialize(triggers));
        return new NoContentResult();
    }


}
