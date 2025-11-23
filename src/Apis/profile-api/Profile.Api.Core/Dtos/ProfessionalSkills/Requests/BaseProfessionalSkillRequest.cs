namespace Profile.Api.Core.Dtos.ProfessionalSkills.Requests;

public record BaseProfessionalSkillRequest(int? Rating, string Name, int YearsOfExperience, string Category);