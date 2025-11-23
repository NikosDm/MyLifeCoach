using FluentValidation;

using Libraries.Common.Constants;

using Profile.Api.Core.Dtos.FinancialProfiles.Requests;

namespace Profile.Api.Core.Validators.FinancialProfiles;

public abstract class BaseFinancialProfileRequestValidator<T>
    : AbstractValidator<T>
    where T : BaseFinancialProfileRequest
{
    protected BaseFinancialProfileRequestValidator()
    {
        RuleFor(x => x.AnnualNetIncome)
            .GreaterThan(0)
            .WithMessage(string.Format(ValidationErrorLiterals.NegativeNumericParameterValue, nameof(BaseFinancialProfileRequest.AnnualNetIncome)));

        RuleFor(x => x.Currency)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BaseFinancialProfileRequest.Currency)));
    }
}