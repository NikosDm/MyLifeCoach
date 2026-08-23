using System;

namespace Profiles.Api.Core.Dtos.Users.Responses;

public sealed record UserResponse(
    Guid UserId,
    string Role,
    bool IsActive);
