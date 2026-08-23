using System;
using System.Collections.Generic;

using Profiles.Api.Core.Dtos.LanguageSkills.Responses;

namespace Profiles.Api.Core.Dtos.PersonalProfiles.Responses;

public sealed record PersonalProfileResponse(
    Guid ProfileId,
    Guid UserId,
    string FullName,
    string Username,
    DateTimeOffset? DateOfBirth,
    string City,
    string Country,
    string Email,
    string PhoneNumber,
    string Role,
    bool IsActive,
    IEnumerable<LanguageSkillResponse> LanguageSkills);
