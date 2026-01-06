using System.Text.Json;
using Acxess.Catalog.Application.Features.AccessTiers.Commands.DeactivateAccessTier;
using Acxess.Catalog.Application.Features.AccessTiers.Queries.GetAccessTiers;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acxess.Web.Pages.Catalog.AccessTiers;
public class IndexModel(IMediator sender) : PageModel
{

    public PaginatedResult<AccessTierDto> Data { get; private set; } = new PaginatedResult<AccessTierDto>(new List<AccessTierDto>(), 0, 1, 10);

    [BindProperty(SupportsGet = true)] public string Search { get; set; } = "";
    [BindProperty(SupportsGet = true)] public string Sort { get; set; } = "name";
    [BindProperty(SupportsGet = true)] public int PageIndex { get; set; } = 1;


    public async Task<IActionResult> OnGet()
    {
        var query = new GetAccessTiersQuery
        (
            Search,
            Sort,
            PageIndex,
            10
        );

        var result = await sender.Send(query);

        if (result.IsSuccess)
        {
            Data = result.Value;
        }

        if (result.IsSuccess && Request.Headers.ContainsKey("HX-Request"))
        {   
            return Partial("_Table", this);
        }
    
        return Page();
    }

    public async Task<IActionResult> OnPostDeactivateAsync(int  id)
    {
        var command = new DeactivateAccessTierCommand(id);
        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description);
            return BadRequest(ModelState);
        }

        var triggers = new
        {
            refreshTable = true, 
            notify = new
            {
                type = "success",
                message = result.Value
            }
        };

        Response.Headers.Append("HX-Trigger", JsonSerializer.Serialize(triggers));
        
        return new NoContentResult();
    }
}
