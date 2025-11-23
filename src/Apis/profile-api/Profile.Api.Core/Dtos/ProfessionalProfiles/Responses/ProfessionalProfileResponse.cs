using System;
using System.Collections.Generic;

using Profile.Api.Core.Dtos.ProfessionalSkills.Responses;

namespace Profile.Api.Core.Dtos.ProfessionalProfiles.Responses;

public sealed record ProfessionalProfileResponse(
    Guid ProfileId,
    Guid UserId,
    string JobTitle,
    int YearsOfExperience,
    string Company,
    IEnumerable<ProfessionalSkillResponse> Skills);

