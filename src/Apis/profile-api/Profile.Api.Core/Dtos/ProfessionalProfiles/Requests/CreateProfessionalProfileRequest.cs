using System.Collections.Generic;

using Profile.Api.Core.Dtos.ProfessionalSkills.Requests;

namespace Profile.Api.Core.Dtos.ProfessionalProfiles.Requests;

public sealed record CreateProfessionalProfileRequest(
    string JobTitle,
    int YearsOfExperience,
    string Company,
    IEnumerable<CreateProfessionalSkillRequest> Skills) : BaseProfessionalProfileRequest(
        JobTitle,
        YearsOfExperience,
        Company);
