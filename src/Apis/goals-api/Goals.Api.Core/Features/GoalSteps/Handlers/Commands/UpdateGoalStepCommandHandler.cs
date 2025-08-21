using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.GoalSteps.Requests;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.GoalSteps.Requests.Commands;
using Goals.Api.Domain.Entities;
using Goals.Api.Domain.ValueObjects;
using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Exceptions;
using Libraries.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace Goals.Api.Core.Features.GoalSteps.Handlers.Commands;

public sealed class UpdateGoalStepCommandHandler(
    IGoalStepRepository goalStepRepository,
    IValidator<UpdateGoalStepRequest> validator,
    ILogger<UpdateGoalStepCommandHandler> logger) 
    : BaseCommandHandler<UpdateGoalStepCommand, GoalStepResponse>(logger), ICommandHandler<UpdateGoalStepCommand, GoalStepResponse>
{
    private readonly IGoalStepRepository _goalStepRepository = goalStepRepository ?? throw new ArgumentNullException(nameof(goalStepRepository));
    private readonly IValidator<UpdateGoalStepRequest> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    public override async Task<GoalStepResponse> Execute(UpdateGoalStepCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await _validator.ValidateAndThrowAsync(request, token);

        var goalStep = await _goalStepRepository.GetByIdAsync(command.Id, token)
            ?? throw new NotFoundException(nameof(GoalStep), command.Id);

        goalStep.Update(
            request.GoalId,
            EntityName.Of(request.Name),
            request.Description,
            request.Order,
            request.DueDate,
            Progress.Of(request.Progress),
            request.Status);

        var result = await _goalStepRepository.UpdateAsync(goalStep, token);

        return result.ToResponse();
    }
}
