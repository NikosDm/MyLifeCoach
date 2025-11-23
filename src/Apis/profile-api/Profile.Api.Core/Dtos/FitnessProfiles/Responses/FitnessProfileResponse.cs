using System;

using Profile.Api.Domain.Enums;

namespace Profile.Api.Core.Dtos.FitnessProfiles.Responses;

public sealed record FitnessProfileResponse(
    Guid ProfileId,
    Guid UserId,
    WeightType WeightType,
    double Weight,
    HeightType HeightType,
    double Height,
    int WorkoutDays,
    string Sport);
