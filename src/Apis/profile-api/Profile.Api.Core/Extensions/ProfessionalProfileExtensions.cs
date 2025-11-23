using System.Linq;

using Profile.Api.Core.Dtos.ProfessionalProfiles.Requests;
using Profile.Api.Core.Dtos.ProfessionalProfiles.Responses;
using Profile.Api.Domain.Models;
using Profile.Api.Domain.Models.Payloads;

namespace Profile.Api.Core.Extensions;

public static class ProfessionalProfileExtensions
{
    public static ProfessionalProfileResponse ToResponse(this ProfessionalProfile source)
        => source is null ? null
        : new ProfessionalProfileResponse(
            source.Id,
            source.UserId,
            source.Payload.JobTitle,
            source.Payload.YearsOfExperience,
            source.Payload.Company,
            source.ProfessionalSkills is null ? []
            : source.ProfessionalSkills.Select(x => x.ToResponse()));

    public static ProfessionalProfile ToEntity(this CreateProfessionalProfileRequest source)
        => source is null ? null
        : new()
        {
            Payload = new ProfessionalProfilePayload
            {
                JobTitle = source.JobTitle,
                YearsOfExperience = source.YearsOfExperience,
                Company = source.Company
            },
            ProfessionalSkills = source.Skills.ToEntities()
        };

    public static ProfessionalProfile MapRequestToEntity(this UpdateProfessionalProfileRequest source, ProfessionalProfile target)
    {
        if (source is null || target is null) return target;

        target.Payload.JobTitle = source.JobTitle;
        target.Payload.YearsOfExperience = source.YearsOfExperience;
        target.Payload.Company = source.Company;
        return target;
    }
}
