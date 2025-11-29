using System;
using System.Collections.Generic;

using Profiles.Api.Core.Dtos.ProfessionalSkills.Responses;

namespace Profiles.Api.Core.Dtos.ProfessionalProfiles.Responses;

public sealed record ProfessionalProfileResponse(
    Guid ProfileId,
    Guid UserId,
    string JobTitle,
    int YearsOfExperience,
    string Company,
    IEnumerable<ProfessionalSkillResponse> Skills);

