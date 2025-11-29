using System;
using System.Collections.Generic;

using Profiles.Api.Core.Dtos.LanguageSkills.Requests;

namespace Profiles.Api.Core.Dtos.PersonalProfiles.Requests;

public sealed record CreatePersonalProfileRequest(
    string FullName,
    DateTimeOffset DateOfBirth,
    string City,
    string Country,
    string Email,
    string PhoneNumber,
    IEnumerable<CreateLanguageSkillRequest> LanguageSkills)
    : BasePersonalProfileRequest(FullName, DateOfBirth, City, Country, Email, PhoneNumber);
