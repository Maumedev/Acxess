using System.ComponentModel.DataAnnotations;

namespace Acxess.Web.Pages.Membership.DigitalExpedient;

public class UpdateMemberInputModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    public string LastName { get; set; } = string.Empty;

    public string? Phone { get; set; }
    public string? Email { get; set; }
}