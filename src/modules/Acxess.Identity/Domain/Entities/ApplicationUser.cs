using System;
using System.ComponentModel.DataAnnotations.Schema;
using Acxess.Shared.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Acxess.Identity.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserNumber { get; set; }
    public string FullName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
    public virtual ICollection<TenantsUsers> TenantsUsers { get; private set; } = new List<TenantsUsers>();

    public static ApplicationUser Create(string userName, string email, string fullName)
    {
        return new ApplicationUser
        {
            UserName = userName,
            Email = email,
            FullName = fullName,
            IsActive = true,
        };
    }
    
    
}
