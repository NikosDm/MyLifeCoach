using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.GoalTypes.Requests;
using Goals.Api.Core.Dtos.GoalTypes.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.GoalTypes.Requests.Commands;
using Goals.Api.Domain.Entities;
using Goals.Api.Domain.ValueObjects;
using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Exceptions;
using Libraries.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace Goals.Api.Core.Features.GoalTypes.Handlers.Commands;

public sealed class UpdateGoalTypeCommandHandler(
    IGoalTypeRepository goalTypeRepository,
    IValidator<UpdateGoalTypeRequest> validator,
    ILogger<UpdateGoalTypeCommandHandler> logger) 
    : BaseCommandHandler<UpdateGoalTypeCommand, GoalTypeResponse>(logger),
    ICommandHandler<UpdateGoalTypeCommand, GoalTypeResponse>
{
    private readonly IGoalTypeRepository _goalTypeRepository = goalTypeRepository ?? throw new ArgumentNullException(nameof(goalTypeRepository));

    private readonly IValidator<UpdateGoalTypeRequest> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    public override async Task<GoalTypeResponse> Execute(UpdateGoalTypeCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await _validator.ValidateAndThrowAsync(request, token);

        var goalType = await _goalTypeRepository.GetByIdAsync(command.Id, token)
            ?? throw new NotFoundException(nameof(GoalType), command.Id);

        goalType.Update(EntityName.Of(request.Name), request.Description, request.IsActive);

        var result = await _goalTypeRepository.UpdateAsync(goalType, token);

        return result.ToResponse();
    }
}
