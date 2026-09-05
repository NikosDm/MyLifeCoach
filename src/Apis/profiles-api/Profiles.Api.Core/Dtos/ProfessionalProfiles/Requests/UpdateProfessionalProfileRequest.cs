using System;

namespace Profiles.Api.Core.Dtos.ProfessionalProfiles.Requests;

public sealed record UpdateProfessionalProfileRequest(
    Guid UserId,
    string JobTitle,
    int YearsOfExperience,
    string Company) : BaseProfessionalProfileRequest(
        UserId,
        JobTitle,
        YearsOfExperience,
        Company);