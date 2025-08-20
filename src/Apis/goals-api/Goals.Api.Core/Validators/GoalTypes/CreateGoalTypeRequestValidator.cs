using System;
using FluentValidation;
using Goals.Api.Core.Dtos.GoalTypes.Requests;

namespace Goals.Api.Core.Validators.GoalTypes;

public sealed class CreateGoalTypeRequestValidator : AbstractValidator<CreateGoalTypeRequest>
{
    public CreateGoalTypeRequestValidator()
    {
         RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Goal type must have a name")
            .MaximumLength(50)
            .WithMessage("Goal type's name should not exceed 50 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("Description should not exceed 1000 characters");
    }
}
