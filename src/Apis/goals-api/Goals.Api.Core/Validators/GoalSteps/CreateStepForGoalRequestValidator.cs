using System;

using FluentValidation;

using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Constants;
using Goals.Api.Core.Dtos.GoalSteps.Requests;
using Goals.Api.Domain.Entities;
using Goals.Api.Domain.Enums;

using Libraries.Common.Constants;

namespace Goals.Api.Core.Validators.GoalSteps;

public sealed class CreateStepForGoalRequestValidator : AbstractValidator<CreateStepForGoalRequest>
{
    private readonly IGoalRepository _goalRepository;
    private readonly TimeProvider _timeProvider;

    public CreateStepForGoalRequestValidator(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
        _timeProvider = TimeProvider.System;

        RuleFor(x => x.GoalId)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(CreateStepForGoalRequest.GoalId)))
            .MustAsync(async (goalId, token) =>
                await _goalRepository.GetByIdAsync(goalId, token) is not null)
            .WithMessage(string.Format(ValidationErrorLiterals.PropDoesNotExist, nameof(Goal)))
            .MustAsync(async (goalId, token) =>
            {
                var goal = await _goalRepository.GetByIdAsync(goalId, token);
                return goal is not null && goal.Status != GoalStatus.Cancelled;
            })
            .WithMessage(GoalValidationErrorLiterals.CannotAddStepsToCancelledOrCompletedGoal);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(CreateStepForGoalRequest.Name)))
            .MaximumLength(50)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(CreateStepForGoalRequest.Name), 50));

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(CreateStepForGoalRequest.Description), 2000));

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0)
            .WithMessage(string.Format(ValidationErrorLiterals.NegativeNumericParameterValue, nameof(CreateStepForGoalRequest.Order)));

        RuleFor(x => x.DueDate)
            .GreaterThan(_timeProvider.GetUtcNow())
            .When(x => x.DueDate.HasValue)
            .WithMessage(string.Format(ValidationErrorLiterals.PastDateNotAllowed, nameof(CreateStepForGoalRequest.DueDate)));
    }
}
