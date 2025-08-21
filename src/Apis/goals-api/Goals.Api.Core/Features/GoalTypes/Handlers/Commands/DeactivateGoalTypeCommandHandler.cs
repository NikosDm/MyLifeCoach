using System;
using System.Threading;
using System.Threading.Tasks;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Features.GoalTypes.Requests.Commands;
using Goals.Api.Domain.Entities;
using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Exceptions;
using Libraries.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace Goals.Api.Core.Features.GoalTypes.Handlers.Commands;

public sealed class DeactivateGoalTypeCommandHandler(
    IGoalTypeRepository goalTypeRepository,
    ILogger<DeactivateGoalTypeCommandHandler> logger) 
    : BaseCommandHandler<DeactivateGoalTypeCommand, Guid>(logger),
     ICommandHandler<DeactivateGoalTypeCommand, Guid>
{
    private readonly IGoalTypeRepository _goalTypeRepository = goalTypeRepository ?? throw new ArgumentNullException(nameof(goalTypeRepository));

    public override async Task<Guid> Execute(DeactivateGoalTypeCommand command, CancellationToken token = default)
    {
        var goalType = await _goalTypeRepository.GetByIdAsync(command.Id, token)
            ?? throw new NotFoundException(nameof(GoalType), command.Id);

        if (!goalType.IsActive)
        {
            throw new BadRequestException("Goal type is already inactive");
        }

        goalType.Deactivate();
        var result = await _goalTypeRepository.UpdateAsync(goalType, token);

        return result.Id;
    }
}
