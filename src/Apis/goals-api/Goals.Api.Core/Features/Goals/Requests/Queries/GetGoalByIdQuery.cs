using System;
using Goals.Api.Core.Dtos.Goals.Responses;
using Libraries.Common.Abstractions.Queries;

namespace Goals.Api.Core.Features.Goals.Requests.Queries;

public sealed record GetGoalByIdQuery(Guid Id) : IQuery<GoalResponse>;
