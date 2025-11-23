using Profile.Api.Domain.Enums;

namespace Profile.Api.Core.Dtos.FitnessProfiles.Requests;

public abstract record BaseFitnessProfileRequest(
    WeightType WeightType,
    double Weight,
    HeightType HeightType,
    double Height,
    int WorkoutDays,
    string Sport);