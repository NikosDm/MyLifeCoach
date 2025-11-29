using System.Collections.Generic;
using System.Linq;

using Profiles.Api.Core.Dtos.ProfessionalSkills.Requests;
using Profiles.Api.Core.Dtos.ProfessionalSkills.Responses;
using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.Core.Extensions;

public static class ProfessionalSkillExtensions
{
    public static ProfessionalSkillResponse ToResponse(this ProfessionalSkill source)
        => source is null ? null
        : new ProfessionalSkillResponse(
            source.Id,
            source.Rating,
            source.Name,
            source.YearsOfExperience,
            source.Category);

    public static ProfessionalSkill ToEntity(this CreateProfessionalSkillRequest source)
        => source is null ? null
        : new()
        {
            Name = source.Name,
            Category = source.Category,
            Rating = source.Rating,
            YearsOfExperience = source.YearsOfExperience
        };

    public static List<ProfessionalSkill> ToEntities(this IEnumerable<CreateProfessionalSkillRequest> source)
        => source is null ? []
        : [.. source.Select(x => x.ToEntity())];

    public static ProfessionalSkill MapRequestToEntity(this UpdateProfessionalSkillRequest source, ProfessionalSkill target)
    {
        if (source is null || target is null) return target;

        target.Name = source.Name;
        target.Category = source.Category;
        target.Rating = source.Rating;
        target.YearsOfExperience = source.YearsOfExperience;
        return target;
    }
}