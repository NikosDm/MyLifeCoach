using Goals.Api.Core.Dtos.Goals.Requests;
using Goals.Api.Core.Dtos.Goals.Responses;
using Libraries.Common.Abstractions.Commands;

namespace Goals.Api.Core.Features.Goals.Requests.Commands;

public sealed record CreateGoalCommand(CreateGoalRequest Request) : ICommand<GoalResponse>;
