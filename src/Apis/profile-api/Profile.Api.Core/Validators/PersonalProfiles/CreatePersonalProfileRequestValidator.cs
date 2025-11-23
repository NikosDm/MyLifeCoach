using FluentValidation;

using Profile.Api.Core.Dtos.LanguageSkills.Requests;
using Profile.Api.Core.Dtos.PersonalProfiles.Requests;

namespace Profile.Api.Core.Validators.PersonalProfiles;

public sealed class CreatePersonalProfileRequestValidator
    : BasePersonalProfileRequestValidator<CreatePersonalProfileRequest>
{
    private readonly IValidator<CreateLanguageSkillRequest> _validator;
    public CreatePersonalProfileRequestValidator(IValidator<CreateLanguageSkillRequest> validator) : base()
    {
        _validator = validator;

        RuleForEach(x => x.LanguageSkills)
              .SetValidator(_validator);
    }
}