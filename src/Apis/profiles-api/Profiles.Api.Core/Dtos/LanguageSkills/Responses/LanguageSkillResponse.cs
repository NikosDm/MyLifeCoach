using System;

namespace Profiles.Api.Core.Dtos.LanguageSkills.Responses;

public sealed record LanguageSkillResponse(Guid Id, int? Rating, string Name, bool? IsNative);
