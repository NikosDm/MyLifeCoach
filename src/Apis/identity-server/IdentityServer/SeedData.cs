using System;
using System.Linq;
using System.Security.Claims;

using IdentityModel;

using IdentityServer.DataAccess.Entities;

using Libraries.Common.Constants;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer;

public static class SeedData
{
    public static void EnsureSeedData(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        // roles
        foreach (var role in SecurityConstants.APPLICATION_ROLES)
        {
            if (!roleManager.RoleExistsAsync(role).Result)
                roleManager.CreateAsync(new ApplicationRole(role)).GetAwaiter().GetResult();
        }

        // admin
        var adminEmail = "admin@lifecoach.com";
        var admin = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();

        if (admin is null)
        {
            admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true, FullName = "Internal Admin", IsActive = true, IsPendingVerification = false };
            var result = userManager.CreateAsync(admin, "admin12345").GetAwaiter().GetResult();

            if (!result.Succeeded)
                throw new Exception("Failed to create admin: " + string.Join(",", result.Errors.Select(e => e.Description)));

            userManager.AddToRoleAsync(admin, SecurityConstants.ADMIN_ROLE).GetAwaiter().GetResult();

            result = userManager.AddClaimsAsync(admin, [
                new Claim(JwtClaimTypes.Name, "Internal Admin"),
                new Claim(JwtClaimTypes.Role, SecurityConstants.ADMIN_ROLE),
                new Claim(SecurityConstants.IS_ACTIVE_CLAIM, "true"),
                new Claim(SecurityConstants.IS_PENDING_VERIFICATION, "false"),
            ]).Result;

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);
        }
    }
}
