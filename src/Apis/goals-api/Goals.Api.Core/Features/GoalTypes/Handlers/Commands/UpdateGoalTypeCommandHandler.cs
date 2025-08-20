using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.GoalTypes.Requests;
using Goals.Api.Core.Dtos.GoalTypes.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.GoalTypes.Requests.Commands;
using Libraries.Common.Abstractions.Commands;
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

        var entity = request.ToEntity();
        var result = await _goalTypeRepository.UpdateAsync(command.Id, entity, token);

        return result.ToResponse();
    }
}
