using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.Goals.Requests;
using Goals.Api.Core.Dtos.Goals.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.Goals.Requests.Commands;
using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace Goals.Api.Core.Features.Goals.Handlers.Commands;

public sealed class UpdateGoalCommandHandler(
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

        var entity = request.ToEntity();

        var result = await _goalRepository.UpdateAsync(command.Id, entity, token);

        return result.ToResponse();
    }
}
