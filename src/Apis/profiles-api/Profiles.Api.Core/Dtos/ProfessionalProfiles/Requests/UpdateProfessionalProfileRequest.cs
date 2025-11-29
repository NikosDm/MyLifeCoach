namespace Profiles.Api.Core.Dtos.ProfessionalProfiles.Requests;

public sealed record UpdateProfessionalProfileRequest(
    string JobTitle,
    int YearsOfExperience,
    string Company) : BaseProfessionalProfileRequest(
        JobTitle,
        YearsOfExperience,
        Company);