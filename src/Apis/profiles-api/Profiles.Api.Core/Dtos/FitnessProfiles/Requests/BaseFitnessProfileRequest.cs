using System;

using Profiles.Api.Domain.Enums;

namespace Profiles.Api.Core.Dtos.FitnessProfiles.Requests;

public abstract record BaseFitnessProfileRequest(
    Guid UserId,
    WeightType WeightType,
    double Weight,
    HeightType HeightType,
    double Height,
    int WorkoutDays,
    string Sport) : BaseProfileRequest(UserId);