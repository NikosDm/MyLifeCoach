using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using IdentityModel;

using IdentityServer.Core.Abstractions;
using IdentityServer.Core.Dtos.Requests;
using IdentityServer.Core.Dtos.Responses;
using IdentityServer.Core.Extensions;
using IdentityServer.DataAccess.Context;
using IdentityServer.DataAccess.Entities;

using Libraries.Common.Constants;
using Libraries.DataInfrastructure.Abstractions;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Core.Services;

internal sealed class AccountService(
    UsersDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IMessageDispatcher messageDispatcher) : IAccountService
{
    private readonly TimeProvider _timeProvider = TimeProvider.System;
    private readonly UsersDbContext _dbContext = dbContext
        ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly UserManager<ApplicationUser> _userManager = userManager
        ?? throw new ArgumentNullException(nameof(userManager));
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager
        ?? throw new ArgumentNullException(nameof(signInManager));
    private readonly IMessageDispatcher _messageDispatcher = messageDispatcher
        ?? throw new ArgumentNullException(nameof(messageDispatcher));

    public async Task<IdentityResult> ChangeUserStatusAsync(Guid userId, bool isActive, DateTimeOffset? deactivationDate = null)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new InvalidOperationException($"User with ID {userId} not found.");
        user.IsActive = isActive;
        user.DeactivationDate = isActive ? null : deactivationDate ?? _timeProvider.GetUtcNow();
        return await _userManager.UpdateAsync(user);
    }

    public async Task<RegisterResponse> CreateAsync(CreateUserRequest request)
    {
        var result = new RegisterResponse(null, IdentityResult.Failed());

        var user = new ApplicationUser
        {
            UserName = request.Username,
            FullName = request.FullName,
            Email = request.Email,
            EmailConfirmed = true, // Default to true for now,
            IsActive = request.IsActive
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);

        if (!createResult.Succeeded)
        {
            return new RegisterResponse(null, createResult);
        }

        await _userManager.AddToRoleAsync(user, request.Role);
        await _userManager.AddClaimsAsync(user,
        [
            new Claim(JwtClaimTypes.Name, request.FullName),
            new Claim(JwtClaimTypes.Role, request.Role),
            new Claim(SecurityConstants.IS_ACTIVE_CLAIM, user.IsActive.ToString())
        ]);

        return result with { User = new UserDto(user.Id, user.UserName, user.Email, request.FullName), Result = createResult };
    }

    public async Task<RegisterResponse> CreateAndDispatchAsync(CreateUserRequest request)
    {
        await using var transaction = await _messageDispatcher.BeginTransactionAsync(_dbContext.Database);

        try
        {
            var response = await CreateAsync(request);

            var headers = new Dictionary<string, string>
            {
                { MessageHeaderConstants.UserId, response.User.ToString() },
                { MessageHeaderConstants.Username, response.User.Username },
                { MessageHeaderConstants.Role, request.Role },
                { MessageHeaderConstants.IsActive, request.ToString() }
            };

            await _messageDispatcher.DispatchAsync(response.ToUserCreatedMessage(request.FullName), headers);
            await transaction.CommitAsync();

            return response;
        }
        catch
        {
            await transaction.RollbackAsync();
            return new RegisterResponse(null, IdentityResult.Failed());
        }
    }

    public async Task<LoginResponse> LoginAsync(LoginUserRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Username);

        user ??= await _userManager.FindByNameAsync(request.Username);

        if (user is null)
        {
            return new LoginResponse(null, SignInResult.Failed);
        }

        if (!user.IsActive || user.DeactivationDate.HasValue)
        {
            return new LoginResponse(null, SignInResult.NotAllowed);
        }

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberLogin, lockoutOnFailure: true);

        return new LoginResponse(new UserDto(user.Id, user.UserName, user.Email, user.FullName), result);
    }
}