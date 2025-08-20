using System;
using FluentValidation;
using Goals.Api.Core.Abstractions.Repositories;
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
            .WithMessage("Goal Id must be present")
            .MustAsync(async (goalId, token) =>
            {
                var goal = await _goalRepository.GetByIdAsync(goalId, token);
                return goal is not null && goal.Status != GoalStatus.Cancelled;
            })
            .WithMessage("Cannot add steps to a cancelled or non existent goal.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, "Goal step's name"))
            .MaximumLength(50)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, "Goal step's name", "50"));

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, "Description", "2000"));

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0)
            .WithMessage(string.Format(ValidationErrorLiterals.NegativeNumericParameterValue, "Order"));

        RuleFor(x => x.Progress)
            .InclusiveBetween(0, 100)
            .WithMessage("Prorgress is a percentage");
    }
}
