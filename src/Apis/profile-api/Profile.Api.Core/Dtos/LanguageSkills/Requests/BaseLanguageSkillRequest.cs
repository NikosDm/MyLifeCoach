namespace Profile.Api.Core.Dtos.LanguageSkills.Requests;

public record BaseLanguageSkillRequest(int? Rating, string LanguageCode, bool? IsNative);