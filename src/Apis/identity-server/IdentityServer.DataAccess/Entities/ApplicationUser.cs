using Microsoft.AspNetCore.Identity;

namespace IdentityServer.DataAccess.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public bool IsActive { get; set; }
    public bool IsPendingVerification { get; set; }
}
