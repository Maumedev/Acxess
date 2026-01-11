using System.Text.Json;
using Acxess.Catalog.Application.Features.AccessTiers.Commands.AddAccessTier;
using Acxess.Catalog.Application.Features.AccessTiers.Commands.UpdateAccessTier;
using Acxess.Catalog.Application.Features.AccessTiers.Queries.GetAccessTiers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acxess.Web.Pages.Catalog.AccessTiers;
public class IndexModel(IMediator sender) : PageModel
{

    public List<AccessTierDto> items { get; private set; } = new();

    [BindProperty]
    public AccessTierInput Input {get; set;}= new();


    public async Task OnGet()
    {
        var query = new GetAccessTiersQuery(true);

        var result = await sender.Send(query);

        if (result.IsSuccess)
        {
            items = result.Value;
        }
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
       if (!ModelState.IsValid) return Partial("_Form", Input); 

        string message = "";
    
        if (Input.IdAccessTier == 0)
        {
            var command = new AddAccessTierCommand(Input.Name, Input.Description);
            var resultSaved = await sender.Send(command);

            if (resultSaved.IsFailure)
            {
                ModelState.AddModelError(string.Empty, resultSaved.Error.Description);
                return Partial("_Form", Input); 
            }

            message = resultSaved.Value;
        }
        else
        {
            var commandUpd = new UpdateAccessTierCommand(Input.IdAccessTier, Input.Name, Input.Description, Input.IsActive);
            var resultSaved = await sender.Send(commandUpd);

            if (resultSaved.IsFailure)
            {
                ModelState.AddModelError(string.Empty, resultSaved.Error.Description);
                return Partial("_Form", Input); 
            }

           message = resultSaved.Value;
        }

        var query = new GetAccessTiersQuery(true);

        var result = await sender.Send(query);

        if (result.IsSuccess) items = result.Value;

        var triggers = new
        {
            listUpdated = items, 
            notify = new { message, type = "success" }
        };
        
        var opciones = new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };
        Response.Headers.Append("HX-Trigger", JsonSerializer.Serialize(triggers, opciones)); 
        return new NoContentResult(); 
    }
}
