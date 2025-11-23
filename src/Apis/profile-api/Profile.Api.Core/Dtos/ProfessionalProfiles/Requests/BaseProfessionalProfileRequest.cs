namespace Profile.Api.Core.Dtos.ProfessionalProfiles.Requests;

public record BaseProfessionalProfileRequest(
    string JobTitle,
    int YearsOfExperience,
    string Company);