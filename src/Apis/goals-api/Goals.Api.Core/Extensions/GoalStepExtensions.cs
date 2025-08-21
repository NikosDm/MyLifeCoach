using System;
using System.Collections.Generic;
using System.Linq;
using Goals.Api.Core.Dtos.GoalSteps.Requests;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Goals.Api.Domain.Entities;
using Goals.Api.Domain.Enums;
using Goals.Api.Domain.ValueObjects;

namespace Goals.Api.Core.Extensions;

public static class GoalStepExtensions
{
    public static GoalStepResponse ToResponse(this GoalStep source)
        => source is null ? null
        : new()
        {
            Id = source.Id,
            Name = source.Name.Value,
            Description = source.Description,
            DueDate = source.DueDate,
            Order = source.Order,
            Status = source.Status,
            Progress = source.Progress.Value
        };

    public static IEnumerable<GoalStepResponse> ToResponse(this IEnumerable<GoalStep> source)
        => source?.Select(x => x.ToResponse());

    public static GoalStep ToEntity(this CreateStepForGoalRequest source)
        => source is null ? null
        : GoalStep.Create(source.GoalId, EntityName.Of(source.Name), source.Description, source.Order, source.DueDate);

    public static CreateStepForGoalRequest ToRequestForGoal(this CreateGoalStepRequest source, Guid goalId)
        => source is null ? null
        : new(goalId, source.Name, source.Description, source.Order, source.DueDate);

    public static GoalStep ToEntity(this CreateGoalStepRequest source)
        => source is null ? null
        : GoalStep.Create(EntityName.Of(source.Name), source.Description, source.Order, source.DueDate);

    public static IEnumerable<GoalStep> ToEntity(this IEnumerable<CreateGoalStepRequest> source)
        => source.Select(x => x.ToEntity());
}
