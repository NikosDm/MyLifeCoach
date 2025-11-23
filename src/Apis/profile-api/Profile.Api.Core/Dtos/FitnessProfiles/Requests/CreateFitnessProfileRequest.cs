using Profile.Api.Domain.Enums;

namespace Profile.Api.Core.Dtos.FitnessProfiles.Requests;

public sealed record CreateFitnessProfileRequest(
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
