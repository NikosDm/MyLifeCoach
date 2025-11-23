using FluentValidation;

using Libraries.Common.Constants;

using Profile.Api.Core.Dtos.LanguageSkills.Requests;

namespace Profile.Api.Core.Validators.LanguageSkills;

public abstract class BaseLanguageSkillRequestValidator<T>
    : AbstractValidator<T>
    where T : BaseLanguageSkillRequest
{
    protected BaseLanguageSkillRequestValidator()
    {
        RuleFor(x => x.LanguageCode)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BaseLanguageSkillRequest.LanguageCode)));

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .When(x => x.Rating != null)
            .WithMessage(string.Format(ValidationErrorLiterals.BetweenParameter, nameof(BaseLanguageSkillRequest.Rating), 1, 5));
    }
}