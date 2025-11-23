using FluentValidation;

using Libraries.Common.Constants;

using Profile.Api.Core.Dtos.FitnessProfiles.Requests;

namespace Profile.Api.Core.Validators.FitnessProfiles;

public abstract class BaseFitnessProfileRequestValidator<T>
    : AbstractValidator<T>
    where T : BaseFitnessProfileRequest
{
    protected BaseFitnessProfileRequestValidator()
    {
        RuleFor(x => x.WeightType)
            .IsInEnum()
            .WithMessage(string.Format(ValidationErrorLiterals.InvalidParameter, nameof(BaseFitnessProfileRequest.WeightType)));

        RuleFor(x => x.HeightType)
            .IsInEnum()
            .WithMessage(string.Format(ValidationErrorLiterals.InvalidParameter, nameof(BaseFitnessProfileRequest.HeightType)));

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .WithMessage(string.Format(ValidationErrorLiterals.NegativeNumericParameterValue, nameof(BaseFitnessProfileRequest.Weight)));

        RuleFor(x => x.Height)
            .GreaterThan(0)
            .WithMessage(string.Format(ValidationErrorLiterals.NegativeNumericParameterValue, nameof(BaseFitnessProfileRequest.Height)));

        RuleFor(x => x.WorkoutDays)
            .InclusiveBetween(0, 7)
            .WithMessage(string.Format(ValidationErrorLiterals.BetweenParameter, nameof(BaseFitnessProfileRequest.WorkoutDays), 0, 7));

        RuleFor(x => x.Sport)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BaseFitnessProfileRequest.Sport)))
            .MaximumLength(100)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(BaseFitnessProfileRequest.Sport), 100));
    }
}