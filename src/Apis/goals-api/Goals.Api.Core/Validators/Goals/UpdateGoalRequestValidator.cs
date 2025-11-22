using System;

using FluentValidation;

using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Constants;
using Goals.Api.Core.Dtos.Goals.Requests;
using Goals.Api.Domain.Entities;

using Libraries.Common.Constants;

namespace Goals.Api.Core.Validators.Goals;

public sealed class UpdateGoalRequestValidator : AbstractValidator<UpdateGoalRequest>
{
    private readonly IGoalTypeRepository _goalTypeRepository;

    public UpdateGoalRequestValidator(IGoalTypeRepository goalTypeRepository)
    {
        _goalTypeRepository = goalTypeRepository ?? throw new ArgumentNullException(nameof(goalTypeRepository));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(UpdateGoalRequest.Name)))
            .MaximumLength(50)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(UpdateGoalRequest.Name), 50));

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => x.Description != null)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(UpdateGoalRequest.Description), 1000));

        RuleFor(x => x.TypeId)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterMustBePresent, nameof(GoalType)))
            .MustAsync(async (typeId, token) =>
            {
                var goalType = await _goalTypeRepository.GetByIdAsync(typeId, token);
                return goalType is not null && goalType.IsActive;
            })
            .WithMessage(GoalValidationErrorLiterals.NonExistentOrInactiveGoalType);

        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .When(x => x.EndDate.HasValue)
            .WithMessage(ValidationErrorLiterals.InvalidGivenPeriod);

        RuleFor(x => x.Status).IsInEnum();

        RuleFor(x => x.Progress)
            .InclusiveBetween(0, 100)
            .WithMessage(string.Format(ValidationErrorLiterals.MustBePercentage, nameof(UpdateGoalRequest.Progress)));
    }
}
