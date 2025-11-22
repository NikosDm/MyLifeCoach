using FluentValidation;

using Goals.Api.Core.Dtos.GoalTypes.Requests;

using Libraries.Common.Constants;

namespace Goals.Api.Core.Validators.GoalTypes;

public sealed class UpdateGoalTypeRequestValidator : AbstractValidator<UpdateGoalTypeRequest>
{
    public UpdateGoalTypeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(UpdateGoalTypeRequest.Name)))
            .MaximumLength(50)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(UpdateGoalTypeRequest.Name), 50));

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(UpdateGoalTypeRequest.Description), 1000));
    }
}
