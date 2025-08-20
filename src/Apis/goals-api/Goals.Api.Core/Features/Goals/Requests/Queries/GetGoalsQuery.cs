using System.Collections.Generic;
using Goals.Api.Core.Dtos.Goals.Responses;
using Libraries.Common.Abstractions.Queries;

namespace Goals.Api.Core.Features.Goals.Requests.Queries;

public sealed record GetGoalsQuery : IQuery<IReadOnlyList<GoalResponse>>;
