using System;

using FluentValidation;

using Libraries.Common.Constants;

using Profiles.Api.Core.Dtos.PersonalProfiles.Requests;

namespace Profiles.Api.Core.Validators.PersonalProfiles;

public abstract class BasePersonalProfileRequestValidator<T> : AbstractValidator<T>
    where T : BasePersonalProfileRequest
{
    private readonly TimeProvider _timeProvider;

    protected BasePersonalProfileRequestValidator()
    {
        _timeProvider = TimeProvider.System;

        RuleFor(x => x.FullName)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BasePersonalProfileRequest.FullName)));

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BasePersonalProfileRequest.DateOfBirth)))
            .Must(date => date < _timeProvider.GetUtcNow())
            .WithMessage(string.Format(ValidationErrorLiterals.FutureDateNotAllowed, nameof(BasePersonalProfileRequest.DateOfBirth)));

        RuleFor(x => x.City)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BasePersonalProfileRequest.City)));

        RuleFor(x => x.Country)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BasePersonalProfileRequest.Country)));

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BasePersonalProfileRequest.Email)))
            .EmailAddress()
            .WithMessage(ValidationErrorLiterals.InvalidEmailAddressFormat);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(ValidationErrorLiterals.NotEmptyParameter, nameof(BasePersonalProfileRequest.PhoneNumber)));
    }
}