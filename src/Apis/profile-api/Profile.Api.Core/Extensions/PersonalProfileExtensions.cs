using System.Linq;

using Profile.Api.Core.Dtos.PersonalProfiles.Requests;
using Profile.Api.Core.Dtos.PersonalProfiles.Responses;
using Profile.Api.Domain.Models;
using Profile.Api.Domain.Models.Payloads;

namespace Profile.Api.Core.Extensions;

public static class PersonalProfileExtensions
{
    public static PersonalProfileResponse ToResponse(this PersonalProfile source)
        => source is null ? null
        : new PersonalProfileResponse(
            source.Id,
            source.UserId,
            source.Payload.FullName,
            source.Payload.DateOfBirth,
            source.Payload.City,
            source.Payload.Country,
            source.Payload.Email,
            source.Payload.PhoneNumber,
            source.LanguageSkills is null ? []
            : source.LanguageSkills.Select(x => x.ToResponse())
        );

    public static PersonalProfile ToEntity(this CreatePersonalProfileRequest source)
        => source is null ? null
        : new()
        {
            Payload = new PersonalProfilePayload
            {
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
}
