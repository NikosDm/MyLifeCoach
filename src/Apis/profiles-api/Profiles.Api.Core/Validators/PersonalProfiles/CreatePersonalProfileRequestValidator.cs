using FluentValidation;

using Profiles.Api.Core.Dtos.LanguageSkills.Requests;
using Profiles.Api.Core.Dtos.PersonalProfiles.Requests;

namespace Profiles.Api.Core.Validators.PersonalProfiles;

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