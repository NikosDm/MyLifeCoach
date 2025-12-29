using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Core.Dtos.Responses;

public sealed record RegisterResponse(UserDto User, IdentityResult Result);