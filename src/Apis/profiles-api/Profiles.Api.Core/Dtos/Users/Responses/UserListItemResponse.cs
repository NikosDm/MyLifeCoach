using System;

namespace Profiles.Api.Core.Dtos.Users.Responses;

public sealed record UserListItemResponse(
    Guid Id,
    string Username,
    string FullName,
    string Email,
    bool IsActive,
    DateTimeOffset? DeactivationDate);