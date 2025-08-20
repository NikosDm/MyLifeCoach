using System;
using Goals.Api.Core.Dtos.GoalSteps.Requests;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Libraries.Common.Abstractions.Commands;

namespace Goals.Api.Core.Features.Goals.Requests.Commands;

public sealed record CreateGoalStepForGoalCommand(CreateStepForGoalRequest Request) : ICommand<GoalStepResponse>;
