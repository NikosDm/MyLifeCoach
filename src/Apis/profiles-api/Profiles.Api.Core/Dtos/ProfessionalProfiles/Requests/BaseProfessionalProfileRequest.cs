using System;

namespace Profiles.Api.Core.Dtos.ProfessionalProfiles.Requests;

public abstract record BaseProfessionalProfileRequest(
    Guid UserId,
    string JobTitle,
    int YearsOfExperience,
    string Company) : BaseProfileRequest(UserId);