using System.Collections.Generic;
using System.Linq;
using Goals.Api.Core.Dtos.GoalTypes.Requests;
using Goals.Api.Core.Dtos.GoalTypes.Responses;
using Goals.Api.Domain.Entities;

namespace Goals.Api.Core.Extensions;

public static class GoalTypeExtensions
{
    public static GoalTypeResponse ToResponse(this GoalType source) =>
        source is null ? null
            : new()
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                IsActive = source.IsActive
            };

    public static IEnumerable<GoalTypeResponse> ToResponse(this IEnumerable<GoalType> source) =>
        source.Select(x => x.ToResponse());

    public static GoalType ToEntity(this CreateGoalTypeRequest source) =>
        source is null ? null
            : new()
            {
                Name = source.Name,
                Description = source.Description,
                IsActive = true
            };

    public static GoalType ToEntity(this UpdateGoalTypeRequest source) =>
        source is null ? null
            : new()
            {
                Name = source.Name,
                Description = source.Description,
                IsActive = source.IsActive
            };
}
