using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

using IdentityModel;

using IdentityServer.DataAccess.Entities;

using Libraries.Common.Constants;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services;

public sealed class UserProfileService(UserManager<ApplicationUser> userManager)
    : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager
        ?? throw new ArgumentNullException(nameof(userManager));

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        var existingClaims = await _userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            new("username", user.UserName)
        };

        context.IssuedClaims.AddRange(claims);
        context.IssuedClaims.Add(existingClaims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name));
        context.IssuedClaims.Add(existingClaims.FirstOrDefault(x => x.Type == JwtClaimTypes.Role));
        context.IssuedClaims.Add(existingClaims.FirstOrDefault(x => x.Type == SecurityConstants.IS_ACTIVE_CLAIM));
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.CompletedTask;
    }
}
