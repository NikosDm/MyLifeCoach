using System;
using System.Linq;

using Libraries.Common.Constants;
using Libraries.Common.Messages;

using Profiles.Api.Core.Dtos.PersonalProfiles.Requests;
using Profiles.Api.Core.Dtos.PersonalProfiles.Responses;
using Profiles.Api.Domain.Models;
using Profiles.Api.Domain.Models.Payloads;

namespace Profiles.Api.Core.Extensions;

public static class PersonalProfileExtensions
{
    public static PersonalProfileResponse ToResponse(this PersonalProfile source)
        => source is null ? null
        : new PersonalProfileResponse(
            source.Id,
            source.UserId,
            source.Payload.FullName,
            source.Payload.Username,
            source.Payload.DateOfBirth,
            source.Payload.City,
            source.Payload.Country,
            source.Payload.Email,
            source.Payload.PhoneNumber,
            source.User.Role,
            source.User.IsActive,
            source.LanguageSkills is null ? []
            : source.LanguageSkills.Select(x => x.ToResponse())
        );

    public static PersonalProfileListItemResponse ToListItemResponse(this PersonalProfile source)
        => source is null ? null
        : new PersonalProfileListItemResponse(
            source.Id,
            source.UserId,
            source.Payload.FullName,
            source.Payload.Username,
            source.Payload.Email,
            source.User.IsActive,
            source.User.DeactivationDate);

    public static PersonalProfile ToEntity(this CreatePersonalProfileRequest source)
    {
        if (source is null) return null;

        var profile = new PersonalProfile
        {
            UserId = source.UserId == Guid.Empty ? Guid.NewGuid() : source.UserId,
            Payload = new PersonalProfilePayload
            {
                Username = source.Username,
                FullName = source.FullName,
                DateOfBirth = source.DateOfBirth,
                City = source.City,
                Country = source.Country,
                Email = source.Email,
                PhoneNumber = source.PhoneNumber
            },
            LanguageSkills = source.LanguageSkills is null ? []
            : source.LanguageSkills.ToEntities()
        };

        if (source.InitialiseUser)
        {
            profile.InitializeUser(source.Role, source.IsActive);
        }

        return profile;
    }

    public static PersonalProfile MapRequestToEntity(this UpdatePersonalProfileRequest source, PersonalProfile target)
    {
        if (source is null || target is null) return target;

        target.Payload.FullName = source.FullName;
        target.Payload.DateOfBirth = source.DateOfBirth;
        target.Payload.City = source.City;
        target.Payload.Country = source.Country;
        target.Payload.Email = source.Email;
        target.Payload.PhoneNumber = source.PhoneNumber;
        return target;
    }

    public static CreatePersonalProfileRequest ToCreateRequest(this UserCreatedMessage source)
        => source is null ? null
        : new CreatePersonalProfileRequest(
            source.Id,
            source.Username,
            source.FullName,
            null,
            null,
            null,
            source.Email,
            null,
            [],
            true,
            SecurityConstants.USER_ROLE,
            false);
}
