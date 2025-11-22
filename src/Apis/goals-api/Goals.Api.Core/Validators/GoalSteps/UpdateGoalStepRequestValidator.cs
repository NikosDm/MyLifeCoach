using System;

using FluentValidation;

using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Constants;
using Goals.Api.Core.Dtos.GoalSteps.Requests;
using Goals.Api.Domain.Enums;

using Libraries.Common.Constants;

namespace Goals.Api.Core.Validators.GoalSteps;

public sealed class UpdateGoalStepRequestValidator : AbstractValidator<UpdateGoalStepRequest>
{
    private readonly IGoalRepository _goalRepository;

    public UpdateGoalStepRequestValidator(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));

        RuleFor(x => x.GoalId)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(UpdateGoalStepRequest.GoalId)))
            .MustAsync(async (goalId, token) =>
            {
                var goal = await _goalRepository.GetByIdAsync(goalId, token);
                return goal is not null && goal.Status != GoalStatus.Cancelled;
            })
            .WithMessage(GoalValidationErrorLiterals.CannotAddStepsToCancelledOrCompletedGoal);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(UpdateGoalStepRequest.Name)))
            .MaximumLength(50)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(UpdateGoalStepRequest.Name), 50));

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(UpdateGoalStepRequest.Description), 2000));

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0)
            .WithMessage(string.Format(ValidationErrorLiterals.NegativeNumericParameterValue, nameof(UpdateGoalStepRequest.Order)));

        RuleFor(x => x.Progress)
            .InclusiveBetween(0, 100)
            .WithMessage(string.Format(ValidationErrorLiterals.MustBePercentage, nameof(UpdateGoalStepRequest.Progress)));
    }
}
