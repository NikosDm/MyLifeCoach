using Microsoft.AspNetCore.Identity;

namespace IdentityServer.DataAccess.Entities;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole()
    { }

    public ApplicationRole(string roleName)
        : base(roleName)
    { }
}
