using System.Collections.Generic;
using Goals.Api.Core.Dtos.GoalTypes.Responses;
using Libraries.Common.Abstractions.Queries;

namespace Goals.Api.Core.Features.GoalTypes.Requests.Queries;

public sealed record GetGoalTypesQuery : IQuery<IReadOnlyList<GoalTypeResponse>>;
