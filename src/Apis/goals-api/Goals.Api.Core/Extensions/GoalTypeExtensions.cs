using System.Collections.Generic;
using System.Linq;
using Goals.Api.Core.Dtos.GoalTypes.Requests;
using Goals.Api.Core.Dtos.GoalTypes.Responses;
using Goals.Api.Domain.Entities;
using Goals.Api.Domain.ValueObjects;

namespace Goals.Api.Core.Extensions;

public static class GoalTypeExtensions
{
    public static GoalTypeResponse ToResponse(this GoalType source) =>
        source is null ? null
            : new()
            {
                Id = source.Id,
                Name = source.Name.Value,
                Description = source.Description,
                IsActive = source.IsActive
            };

    public static IEnumerable<GoalTypeResponse> ToResponse(this IEnumerable<GoalType> source) =>
        source.Select(x => x.ToResponse());

    public static GoalType ToEntity(this CreateGoalTypeRequest source) =>
        source is null ? null
            : GoalType.Create(EntityName.Of(source.Name), source.Description);
}
