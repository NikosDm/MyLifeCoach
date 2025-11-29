using System;

namespace Profiles.Api.Core.Dtos.ProfessionalSkills.Responses;

public sealed record ProfessionalSkillResponse(Guid Id, int? Rating, string Name, int YearsOfExperience, string Category);
