using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Core.Dtos.Responses;

public sealed record LoginResponse(UserDto User, SignInResult Result);