using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Acxess.Core.Modules.Identity.Entities;

public class ApplicationUser : IdentityUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserNumber { get; set; }
    public int? TenantId { get; set; }
    public string FullName { get; set; }
    public bool Active { get; set; }
}
