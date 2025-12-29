using System;
using System.Security.Claims;
using System.Threading.Tasks;

using IdentityModel;

using IdentityServer.Core.Abstractions;
using IdentityServer.Core.Dtos.Requests;
using IdentityServer.Core.Dtos.Responses;
using IdentityServer.DataAccess.Entities;

using Libraries.Common.Constants;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Core.Services;

internal sealed class AccountService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager
        ?? throw new ArgumentNullException(nameof(userManager));
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager
        ?? throw new ArgumentNullException(nameof(signInManager));

    public async Task<RegisterResponse> CreateAsync(CreateUserRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            EmailConfirmed = true, // Default to true for now,
            IsActive = true,
            IsPendingVerification = true,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, SecurityConstants.USER_ROLE);

            await _userManager.AddClaimsAsync(user,
            [
                new Claim(JwtClaimTypes.Name, request.FullName),
                new Claim(JwtClaimTypes.Role, SecurityConstants.USER_ROLE),
                new Claim(SecurityConstants.IS_ACTIVE_CLAIM, user.IsActive.ToString()),
                new Claim(SecurityConstants.IS_PENDING_VERIFICATION, user.IsPendingVerification.ToString()),
            ]);
        }

        return new RegisterResponse(new UserDto(user.Id, user.UserName, user.Email, user.FullName), result);
    }

    public async Task<LoginResponse> LoginAsync(LoginUserRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Username);

        user ??= await _userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            return new LoginResponse(null, SignInResult.Failed);
        }

        if (!user.IsActive || user.IsPendingVerification)
        {
            return new LoginResponse(null, SignInResult.NotAllowed);
        }

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberLogin, lockoutOnFailure: true);

        return new LoginResponse(new UserDto(user.Id, user.UserName, user.Email, user.FullName), result);
    }
}