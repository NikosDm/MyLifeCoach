using FluentValidation;

using Libraries.Common.Constants;

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

        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(CreatePersonalProfileRequest.Username)));

        RuleForEach(x => x.LanguageSkills)
              .SetValidator(_validator);

        RuleFor(x => x.Role)
            .NotEmpty()
            .NotNull()
            .When(x => x.InitialiseUser)
            .Must(x => x.Equals(SecurityConstants.USER_ROLE) || x.Equals(SecurityConstants.ADMIN_ROLE))
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(CreatePersonalProfileRequest.Role)));
    }
}