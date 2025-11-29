namespace Profiles.Api.Core.Dtos.LanguageSkills.Requests;

public sealed record CreateLanguageSkillRequest(int? Rating, string Name, bool? IsNative)
    : BaseLanguageSkillRequest(Rating, Name, IsNative);
