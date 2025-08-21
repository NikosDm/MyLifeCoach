using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.Goals.Requests;
using Goals.Api.Core.Dtos.Goals.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.Goals.Requests.Commands;
using Goals.Api.Domain.Entities;
using Goals.Api.Domain.ValueObjects;
using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Exceptions;
using Libraries.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace Goals.Api.Core.Features.Goals.Handlers.Commands;

internal sealed class UpdateGoalCommandHandler(
    IGoalRepository goalRepository,
    IValidator<UpdateGoalRequest> validator,
    ILogger<UpdateGoalCommandHandler> logger) 
    : BaseCommandHandler<UpdateGoalCommand, GoalResponse>(logger), ICommandHandler<UpdateGoalCommand, GoalResponse>
{
    private readonly IGoalRepository _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
    private readonly IValidator<UpdateGoalRequest> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    public override async Task<GoalResponse> Execute(UpdateGoalCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await _validator.ValidateAndThrowAsync(request, token);

        var goal = await _goalRepository.GetByIdAsync(command.Id, token)
            ?? throw new NotFoundException(nameof(Goal), command.Id);

        goal.Update(
            EntityName.Of(request.Name),
            request.Description,
            request.TypeId,
            GoalPeriod.Of(request.StartDate, request.EndDate),
            request.Status);
            
        var result = await _goalRepository.UpdateAsync(goal, token);

        return result.ToResponse();
    }
}
