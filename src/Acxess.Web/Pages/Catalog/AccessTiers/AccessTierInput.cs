using System;
using System.ComponentModel.DataAnnotations;

namespace Acxess.Web.Pages.Catalog.AccessTiers;

public class AccessTierInput
{
    public int IdAccessTier { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio. ")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}