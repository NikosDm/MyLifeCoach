using System;
using System.Collections.Generic;

using Profiles.Api.Core.Dtos.LanguageSkills.Responses;

namespace Profiles.Api.Core.Dtos.PersonalProfiles.Responses;

public sealed record PersonalProfileResponse(
    Guid ProfileId,
    Guid UserId,
    string FullName,
    DateTimeOffset DateOfBirth,
    string City,
    string Country,
    string Email,
    string PhoneNumber,
    IEnumerable<LanguageSkillResponse> LanguageSkills);
