using System;
using System.Collections.Generic;
using System.Linq;
using Goals.Api.Core.Dtos.GoalSteps.Requests;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Goals.Api.Domain.Entities;
using Goals.Api.Domain.Enums;

namespace Goals.Api.Core.Extensions;

public static class GoalStepExtensions
{
    public static GoalStepResponse ToResponse(this GoalStep source)
        => source is null ? null
        : new()
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            DueDate = source.DueDate,
            Order = source.Order,
            Status = source.Status,
            Progress = source.Progress
        };

    public static IEnumerable<GoalStepResponse> ToResponse(this IEnumerable<GoalStep> source)
        => source?.Select(x => x.ToResponse());

    public static GoalStep ToEntity(this CreateStepForGoalRequest source)
        => source is null ? null
        : new()
        {
            Name = source.Name,
            Description = source.Description,
            Order = source.Order,
            DueDate = source.DueDate,
            Status = GoalStepStatus.NotStarted,
            Progress = 0,
            GoalId = source.GoalId
        };

    public static CreateStepForGoalRequest ToRequestForGoal(this CreateGoalStepRequest source, Guid goalId)
        => source is null ? null
        : new(goalId, source.Name, source.Description, source.Order, source.DueDate);

    public static GoalStep ToEntity(this CreateGoalStepRequest source)
        => source is null ? null
        : new()
        {
            Name = source.Name,
            Description = source.Description,
            Order = source.Order,
            DueDate = source.DueDate,
            Status = GoalStepStatus.NotStarted,
            Progress = 0
        };

    public static IEnumerable<GoalStep> ToEntity(this IEnumerable<CreateGoalStepRequest> source)
        => source.Select(x => x.ToEntity());

        
    public static GoalStep ToEntity(this UpdateGoalStepRequest source)
        => source is null ? null
        : new()
        {
            Name = source.Name,
            Description = source.Description,
            Order = source.Order,
            DueDate = source.DueDate,
            Status = source.Status,
            Progress = source.Progress
        };
}
