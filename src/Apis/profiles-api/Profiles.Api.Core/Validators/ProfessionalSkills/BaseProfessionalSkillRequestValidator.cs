using FluentValidation;

using Libraries.Common.Constants;

using Profiles.Api.Core.Dtos.ProfessionalSkills.Requests;

namespace Profiles.Api.Core.Validators.ProfessionalSkills;

public abstract class BaseProfessionalSkillRequestValidator<T> : AbstractValidator<T>
    where T : BaseProfessionalSkillRequest
{
    protected BaseProfessionalSkillRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BaseProfessionalSkillRequest.Name)));

        RuleFor(x => x.YearsOfExperience)
            .GreaterThanOrEqualTo(0)
            .WithMessage(string.Format(ValidationErrorLiterals.NegativeNumericParameterValue, nameof(BaseProfessionalSkillRequest.YearsOfExperience)));

        RuleFor(x => x.Rating)
           .InclusiveBetween(1, 5)
           .When(x => x.Rating != null)
           .WithMessage(string.Format(ValidationErrorLiterals.BetweenParameter, nameof(BaseProfessionalSkillRequest.Rating), 1, 5));
    }
}