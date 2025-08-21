using System;
using Libraries.Common.Abstractions.Commands;

namespace Goals.Api.Core.Features.GoalSteps.Requests.Commands;

public sealed record DeleteGoalStepCommand(Guid Id) : ICommand<Guid>;
