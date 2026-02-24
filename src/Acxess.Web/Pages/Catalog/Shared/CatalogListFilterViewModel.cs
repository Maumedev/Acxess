namespace Acxess.Web.Pages.Catalog.Shared;

public class CatalogListFilterViewModel
{
    public string SearchValue { get; set; } = string.Empty;
    public required string FilterUrl { get; set; }
    public required string CreateUrl { get; set; }
    public string TableTarget { get; set; } = "#table-content";
    public string CreateButtonText { get; set; } = "Nuevo";
}