using System.Text.Json;
using Acxess.Catalog.Application.Features.AccessTiers.Commands.UpdateAccessTier;
using Acxess.Catalog.Application.Features.AccessTiers.Queries.GetAccesTierById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acxess.Web.Pages.Catalog.AccessTiers;
public class EditModel(IMediator mediator) : PageModel
{
    [BindProperty]
    public CreateSellingPlanInput Input { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public IActionResult OnGet()
    {
        var query = new GetAccesTierByIdQuery(Id);
        var result = mediator.Send(query).Result;

        if (result.IsFailure)
        {
            return NotFound();
        }

        Input.Name = result.Value.Name;
        Input.Description = result.Value.Description;

        if (Request.Headers.ContainsKey("HX-Request"))
        {
            return Partial("_Edit", this);
        }
        
        return Page();
    }


    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Partial("_Edit", this);
        }

        var command = new UpdateAccessTierCommand(
            Id,
            Input.Name,
            Input.Description
        );

        var result = await mediator.Send(command);
        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description);
            return Partial("_Edit", this);
        }

        var triggers = new { 
            closeSlide = true,      
            refreshTable = true,
              notify = new { 
                type = "success", 
                message = result.Value
            }
        };
        
        Response.Headers.Append("HX-Trigger", JsonSerializer.Serialize(triggers));
        return new NoContentResult();
    }
}
