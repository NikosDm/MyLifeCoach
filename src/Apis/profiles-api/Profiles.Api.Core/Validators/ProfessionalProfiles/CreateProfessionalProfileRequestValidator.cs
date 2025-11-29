using FluentValidation;

using Profiles.Api.Core.Dtos.ProfessionalProfiles.Requests;
using Profiles.Api.Core.Dtos.ProfessionalSkills.Requests;

namespace Profiles.Api.Core.Validators.ProfessionalProfiles;

public sealed class CreateProfessionalProfileRequestValidator
    : BaseProfessionalProfileRequestValidator<CreateProfessionalProfileRequest>
{
    private readonly IValidator<CreateProfessionalSkillRequest> _skillValidator;

    public CreateProfessionalProfileRequestValidator(
        IValidator<CreateProfessionalSkillRequest> skillValidator) : base()
    {
        _skillValidator = skillValidator;

        RuleForEach(x => x.Skills)
            .SetValidator(_skillValidator);
    }
}