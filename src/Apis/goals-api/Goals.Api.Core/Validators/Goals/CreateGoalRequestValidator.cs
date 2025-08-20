using System;
using FluentValidation;
using Goals.Api.Core.Abstractions.Repositories;
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
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, "Name"))
            .MaximumLength(50)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, "Name", "50"));

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => x.Description != null)
            .WithMessage(string.Format(ValidationErrorLiterals.ParameterExceedLimit, "Description", "1000"));

        RuleFor(x => x.TypeId)
            .NotEmpty()
            .WithMessage("A type must be selected")
            .MustAsync(async (typeId, token) =>
            {
                var goalType = await _goalTypeRepository.GetByIdAsync(typeId, token);
                return goalType is not null && goalType.IsActive;
            })
            .WithMessage("Given type does not exist or is inactive");

        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .When(x => x.EndDate.HasValue)
            .WithMessage("EndDate must be later than StartDate.");

        RuleForEach(x => x.Steps)
            .SetValidator(_stepValidator);
    }
}
