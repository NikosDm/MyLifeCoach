using System.Collections.Generic;
using System.Linq;

using Goals.Api.Core.Dtos.Goals.Requests;
using Goals.Api.Core.Dtos.Goals.Responses;
using Goals.Api.Domain.Entities;
using Goals.Api.Domain.ValueObjects;

namespace Goals.Api.Core.Extensions;

public static class GoalExtensions
{
    public static GoalResponse ToResponse(this Goal source)
        => source is null ? null
        : new()
        {
            Id = source.Id,
            Name = source.Name.Value,
            Description = source.Description,
            StartDate = source.Period.StartDate,
            EndDate = source.Period.EndDate,
            Status = source.Status,
            TypeId = source.TypeId,
            Progress = source.Progress.Value,
            Steps = source.Steps.ToResponse()
        };

    public static IEnumerable<GoalResponse> ToResponse(this IEnumerable<Goal> source)
        => source?.Select(x => x.ToResponse());

    public static Goal ToEntity(this CreateGoalRequest source)
    {
        if (source is null) return null;

        var goal = Goal.Create(
            EntityName.Of(source.Name),
            source.Description,
            source.TypeId,
            GoalPeriod.Of(source.StartDate, source.EndDate));

        foreach (var step in source.Steps)
            goal.AddGoalStep(step.ToEntity());

        return goal;
    }
}
