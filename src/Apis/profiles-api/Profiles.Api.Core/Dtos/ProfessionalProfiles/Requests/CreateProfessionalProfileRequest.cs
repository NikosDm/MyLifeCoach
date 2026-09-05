using System;
using System.Collections.Generic;

using Profiles.Api.Core.Dtos.ProfessionalSkills.Requests;

namespace Profiles.Api.Core.Dtos.ProfessionalProfiles.Requests;

public sealed record CreateProfessionalProfileRequest(
    Guid UserId,
    string JobTitle,
    int YearsOfExperience,
    string Company,
    IEnumerable<CreateProfessionalSkillRequest> Skills) : BaseProfessionalProfileRequest(
        UserId,
        JobTitle,
        YearsOfExperience,
        Company);
