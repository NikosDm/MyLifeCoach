using Profile.Api.Core.Dtos.FitnessProfiles.Requests;
using Profile.Api.Core.Dtos.FitnessProfiles.Responses;
using Profile.Api.Domain.Models;
using Profile.Api.Domain.Models.Payloads;

namespace Profile.Api.Core.Extensions;

public static class FitnessProfileExtensions
{
    public static FitnessProfileResponse ToResponse(this FitnessProfile source)
        => source is null ? null
        : new FitnessProfileResponse(
            source.Id,
            source.UserId,
            source.Payload.WeightType,
            source.Payload.Weight,
            source.Payload.HeightType,
            source.Payload.Height,
            source.Payload.WorkoutDays,
            source.Payload.Sport);

    public static FitnessProfile ToEntity(this CreateFitnessProfileRequest source)
        => source is null ? null
        : new()
        {
            Payload = new FitnessProfilePayload
            {
                WeightType = source.WeightType,
                Weight = source.Weight,
                HeightType = source.HeightType,
                Height = source.Height,
                WorkoutDays = source.WorkoutDays,
                Sport = source.Sport
            }
        };

    public static FitnessProfile MapRequestToEntity(this UpdateFitnessProfileRequest source, FitnessProfile target)
    {
        if (source is null || target is null) return target;

        target.Payload.WeightType = source.WeightType;
        target.Payload.Weight = source.Weight;
        target.Payload.HeightType = source.HeightType;
        target.Payload.Height = source.Height;
        target.Payload.WorkoutDays = source.WorkoutDays;
        target.Payload.Sport = source.Sport;
        return target;
    }
}
