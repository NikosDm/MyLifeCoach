using System;

namespace IdentityServer.Core.Dtos.Responses;

public sealed record UserDto(string Id, string Username, string Email, string FullName);