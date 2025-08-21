using System;
using System.Threading;
using System.Threading.Tasks;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Features.GoalSteps.Requests.Commands;
using Goals.Api.Domain.Entities;
using Goals.Api.Domain.Enums;
using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Exceptions;
using Libraries.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace Goals.Api.Core.Features.GoalSteps.Handlers.Commands;

public sealed class DeleteGoalStepCommandHandler(
    ILogger<BaseCommandHandler<DeleteGoalStepCommand, Guid>> logger,
    IGoalStepRepository goalStepRepository)
    : BaseCommandHandler<DeleteGoalStepCommand, Guid>(logger), ICommandHandler<DeleteGoalStepCommand, Guid>
{
    private readonly IGoalStepRepository _goalStepRepository = goalStepRepository
        ?? throw new ArgumentNullException(nameof(goalStepRepository));

    public override async Task<Guid> Execute(DeleteGoalStepCommand command, CancellationToken token = default)
    {
        var goalStepId = command.Id;

        var goalStep = await _goalStepRepository.GetByIdAsync(goalStepId, token)
            ?? throw new NotFoundException(nameof(GoalStep), goalStepId);

        if (goalStep.Status == GoalStepStatus.Deleted) throw new BadRequestException("Goal step is already deleted");

        goalStep.Status = GoalStepStatus.Deleted;
        var result = await _goalStepRepository.UpdateAsync(goalStepId, goalStep, token);

        return result.Id;
    }
}
