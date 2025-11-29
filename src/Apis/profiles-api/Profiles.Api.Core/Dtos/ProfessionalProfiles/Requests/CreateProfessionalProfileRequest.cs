using System.Collections.Generic;

using Profiles.Api.Core.Dtos.ProfessionalSkills.Requests;

namespace Profiles.Api.Core.Dtos.ProfessionalProfiles.Requests;

public sealed record CreateProfessionalProfileRequest(
    string JobTitle,
    int YearsOfExperience,
    string Company,
    IEnumerable<CreateProfessionalSkillRequest> Skills) : BaseProfessionalProfileRequest(
        JobTitle,
        YearsOfExperience,
        Company);
