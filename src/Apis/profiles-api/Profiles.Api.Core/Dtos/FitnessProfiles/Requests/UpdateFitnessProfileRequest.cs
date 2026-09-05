using System;

using Profiles.Api.Domain.Enums;

namespace Profiles.Api.Core.Dtos.FitnessProfiles.Requests;

public sealed record UpdateFitnessProfileRequest(
    Guid UserId,
    WeightType WeightType,
    double Weight,
    HeightType HeightType,
    double Height,
    int WorkoutDays,
    string Sport) : BaseFitnessProfileRequest(
        UserId,
        WeightType,
        Weight,
        HeightType,
        Height,
        WorkoutDays,
        Sport);