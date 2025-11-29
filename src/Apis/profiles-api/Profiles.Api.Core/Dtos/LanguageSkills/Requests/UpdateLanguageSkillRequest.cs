namespace Profiles.Api.Core.Dtos.LanguageSkills.Requests;

public sealed record UpdateLanguageSkillRequest(int? Rating, string LanguageCode, bool? IsNative)
    : BaseLanguageSkillRequest(Rating, LanguageCode, IsNative);