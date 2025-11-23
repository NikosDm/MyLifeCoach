using System.Collections.Generic;

namespace Profile.Api.Core.Dtos.ProfessionalSkills.Requests;

public sealed record CreateProfessionalSkillRequest(
    int? Rating,
    string Name,
    int YearsOfExperience,
    string Category)
    : BaseProfessionalSkillRequest(Rating, Name, YearsOfExperience, Category);
