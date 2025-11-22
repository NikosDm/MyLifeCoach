using System;

using FluentValidation;

using Goals.Api.Core.Dtos.GoalSteps.Requests;

using Libraries.Common.Constants;

namespace Goals.Api.Core.Validators.GoalSteps;

public sealed class CreateGoalStepRequestValidator : AbstractValidator<CreateGoalStepRequest>
{
    private readonly TimeProvider _timeProvider;

    public CreateGoalStepRequestValidator()
    {
        _timeProvider = TimeProvider.System;

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(CreateGoalStepRequest.Name)))
            .MaximumLength(50)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(CreateGoalStepRequest.Name), 50));

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(CreateGoalStepRequest.Description), 2000));

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0)
            .WithMessage(string.Format(ValidationErrorLiterals.NegativeNumericParameterValue, nameof(CreateGoalStepRequest.Order)));

        RuleFor(x => x.DueDate)
            .LessThan(_timeProvider.GetUtcNow())
            .When(x => x.DueDate.HasValue)
            .WithMessage(string.Format(ValidationErrorLiterals.PastDateNotAllowed, nameof(CreateGoalStepRequest.DueDate)));
    }
}
