using Profiles.Api.Domain.Enums;

namespace Profiles.Api.Core.Dtos.FitnessProfiles.Requests;

public sealed record UpdateFitnessProfileRequest(
    WeightType WeightType,
    double Weight,
    HeightType HeightType,
    double Height,
    int WorkoutDays,
    string Sport) : BaseFitnessProfileRequest(
        WeightType,
        Weight,
        HeightType,
        Height,
        WorkoutDays,
        Sport);