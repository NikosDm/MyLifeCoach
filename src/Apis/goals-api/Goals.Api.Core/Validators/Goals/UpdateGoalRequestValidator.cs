using System;
using FluentValidation;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.Goals.Requests;

namespace Goals.Api.Core.Validators.Goals;

public sealed class UpdateGoalRequestValidator : AbstractValidator<UpdateGoalRequest>
{
    private readonly IGoalTypeRepository _goalTypeRepository;

    public UpdateGoalRequestValidator(IGoalTypeRepository goalTypeRepository)
    {
        _goalTypeRepository = goalTypeRepository ?? throw new ArgumentNullException(nameof(goalTypeRepository));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name should not be empty")
            .MaximumLength(50)
            .WithMessage("Name should not exceed 50 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => x.Description != null)
            .WithMessage("Description should not exceed 1000 characters");

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

        RuleFor(x => x.Status).IsInEnum();

        RuleFor(x => x.Progress)
            .InclusiveBetween(0, 100)
            .WithMessage("Prorgress is a percentage, it should be between 0 and 100");
    }
}
