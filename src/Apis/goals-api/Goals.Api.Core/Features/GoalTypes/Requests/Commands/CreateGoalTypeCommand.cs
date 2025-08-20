using Goals.Api.Core.Dtos.GoalTypes.Requests;
using Goals.Api.Core.Dtos.GoalTypes.Responses;
using Libraries.Common.Abstractions.Commands;

namespace Goals.Api.Core.Features.GoalTypes.Requests.Commands;

public sealed record CreateGoalTypeCommand(CreateGoalTypeRequest Request) : ICommand<GoalTypeResponse>;
