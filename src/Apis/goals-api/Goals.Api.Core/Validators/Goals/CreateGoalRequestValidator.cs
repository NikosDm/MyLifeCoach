using System;

using FluentValidation;

using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Constants;
using Goals.Api.Core.Dtos.Goals.Requests;
using Goals.Api.Core.Dtos.GoalSteps.Requests;

using Libraries.Common.Constants;

namespace Goals.Api.Core.Validators.Goals;

public sealed class CreateGoalRequestValidator : AbstractValidator<CreateGoalRequest>
{
    private readonly IGoalTypeRepository _goalTypeRepository;
    private readonly IValidator<CreateGoalStepRequest> _stepValidator;

    public CreateGoalRequestValidator(IGoalTypeRepository goalTypeRepository, IValidator<CreateGoalStepRequest> stepValidator)
    {
        _goalTypeRepository = goalTypeRepository ?? throw new ArgumentNullException(nameof(goalTypeRepository));
        _stepValidator = stepValidator ?? throw new ArgumentNullException(nameof(stepValidator));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(CreateGoalRequest.Name)))
            .MaximumLength(50)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(CreateGoalRequest.Name), 50));

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => x.Description != null)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, nameof(CreateGoalRequest.Description), 1000));

        RuleFor(x => x.TypeId)
            .NotEmpty()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(CreateGoalRequest.TypeId)))
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

        RuleForEach(x => x.Steps)
            .SetValidator(_stepValidator);
    }
}
