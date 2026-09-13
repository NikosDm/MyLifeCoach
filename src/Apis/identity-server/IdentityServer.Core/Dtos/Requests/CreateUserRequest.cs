using System;

using Libraries.Common.Constants;

namespace IdentityServer.Core.Dtos.Requests;

public sealed record CreateUserRequest
{
    public string Id { get; set; }
    public string Email { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string FullName { get; init; }
    public bool IsActive { get; init; } = false;
    public string Role { get; init; } = SecurityConstants.USER_ROLE;
}