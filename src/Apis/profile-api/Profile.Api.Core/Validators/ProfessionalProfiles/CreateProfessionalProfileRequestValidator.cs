using FluentValidation;

using Profile.Api.Core.Dtos.ProfessionalProfiles.Requests;
using Profile.Api.Core.Dtos.ProfessionalSkills.Requests;

namespace Profile.Api.Core.Validators.ProfessionalProfiles;

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