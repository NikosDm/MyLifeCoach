using System;
using System.Threading.Tasks;

using IdentityServer.Core.Dtos.Requests;
using IdentityServer.Core.Dtos.Responses;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Core.Abstractions;

public interface IAccountService
{
    Task<RegisterResponse> CreateAndDispatchAsync(CreateUserRequest request);
    Task<RegisterResponse> CreateAsync(CreateUserRequest request);
    Task<LoginResponse> LoginAsync(LoginUserRequest request);
    Task<IdentityResult> ChangeUserStatusAsync(Guid userId, bool isActive, DateTimeOffset? deactivationDate = null);
}