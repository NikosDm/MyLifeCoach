using System;
using Libraries.Common.Abstractions.Commands;

namespace Goals.Api.Core.Features.GoalTypes.Requests.Commands;

public sealed record DeactivateGoalTypeCommand(Guid Id) : ICommand<Guid>;
