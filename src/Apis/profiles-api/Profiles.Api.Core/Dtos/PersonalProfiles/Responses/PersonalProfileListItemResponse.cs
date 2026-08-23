using System;

namespace Profiles.Api.Core.Dtos.PersonalProfiles.Responses;

public sealed record PersonalProfileListItemResponse(
    Guid Id,
    Guid UserId,
    string FullName,
    string Username,
    string Email,
    bool IsActive,
    DateTimeOffset? DeactivationDate);