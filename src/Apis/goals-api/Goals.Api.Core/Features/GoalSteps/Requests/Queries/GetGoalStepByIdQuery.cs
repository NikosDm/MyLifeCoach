using System;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Libraries.Common.Abstractions.Queries;

namespace Goals.Api.Core.Features.GoalSteps.Requests.Queries;

public sealed record GetGoalStepByIdQuery(Guid Id) : IQuery<GoalStepResponse>;
