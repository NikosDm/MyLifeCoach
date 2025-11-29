using System.Collections.Generic;
using System.Linq;

using Profiles.Api.Core.Dtos.LanguageSkills.Requests;
using Profiles.Api.Core.Dtos.LanguageSkills.Responses;
using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.Core.Extensions;

public static class LanguageSkillExtensions
{
    public static LanguageSkillResponse ToResponse(this LanguageSkill source)
        => source is null ? null
        : new LanguageSkillResponse(
            source.Id,
            source.Rating,
            source.LanguageCode,
            source.IsNative);

    public static LanguageSkill ToEntity(this CreateLanguageSkillRequest source)
        => source is null ? null
        : new()
        {
            LanguageCode = source.Name,
            IsNative = source.IsNative,
            Rating = source.Rating
        };

    public static List<LanguageSkill> ToEntities(this IEnumerable<CreateLanguageSkillRequest> source)
        => source is null ? []
        : [.. source.Select(x => x.ToEntity())];

    public static LanguageSkill MapRequestToEntity(this UpdateLanguageSkillRequest source, LanguageSkill target)
    {
        if (source is null || target is null) return target;

        target.Rating = source.Rating;
        target.LanguageCode = source.LanguageCode;
        target.IsNative = source.IsNative;
        return target;
    }
}
