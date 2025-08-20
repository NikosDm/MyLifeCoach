using System.Collections.Generic;
using System.Linq;
using Goals.Api.Core.Dtos.Goals.Requests;
using Goals.Api.Core.Dtos.Goals.Responses;
using Goals.Api.Domain.Entities;
using Goals.Api.Domain.Enums;

namespace Goals.Api.Core.Extensions;

public static class GoalExtensions
{
    public static GoalResponse ToResponse(this Goal source)
        => source is null ? null
        : new()
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            StartDate = source.StartDate,
            EndDate = source.EndDate,
            Status = source.Status,
            Type = source.Type?.Name,
            Progress = source.Progress,
            Steps = source.Steps.ToResponse()
        };

    public static IEnumerable<GoalResponse> ToResponse(this IEnumerable<Goal> source)
        => source?.Select(x => x.ToResponse());

    public static Goal ToEntity(this CreateGoalRequest source)
        => source is null ? null
        : new()
        {
            Name = source.Name,
            Description = source.Description,
            TypeId = source.TypeId,
            StartDate = source.StartDate,
            EndDate = source.EndDate,
            Status = GoalStatus.NotStarted,
            Progress = 0,
            Steps = source.Steps.ToEntity().ToList()
        };

    public static Goal ToEntity(this UpdateGoalRequest source)
        => source is null ? null
        : new()
        {
            Name = source.Name,
            Description = source.Description,
            TypeId = source.TypeId,
            StartDate = source.StartDate,
            EndDate = source.EndDate,
            Status = source.Status,
            Progress = source.Progress
        };
}
