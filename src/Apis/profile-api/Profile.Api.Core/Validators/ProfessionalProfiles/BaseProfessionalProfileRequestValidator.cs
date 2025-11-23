using FluentValidation;

using Libraries.Common.Constants;

using Profile.Api.Core.Dtos.ProfessionalProfiles.Requests;

namespace Profile.Api.Core.Validators.ProfessionalProfiles;

public abstract class BaseProfessionalProfileRequestValidator<T>
    : AbstractValidator<T> where T : BaseProfessionalProfileRequest
{
    protected BaseProfessionalProfileRequestValidator()
    {
        RuleFor(x => x.JobTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BaseProfessionalProfileRequest.JobTitle)));

        RuleFor(x => x.YearsOfExperience)
            .GreaterThanOrEqualTo(0)
            .WithMessage(string.Format(ValidationErrorLiterals.NegativeNumericParameterValue, nameof(BaseProfessionalProfileRequest.YearsOfExperience)));

        RuleFor(x => x.Company)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BaseProfessionalProfileRequest.Company)));
    }
}