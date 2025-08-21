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

internal sealed class CreateGoalCommandHandler(
    IGoalRepository goalRepository,
    IValidator<CreateGoalRequest> validator,
    ILogger<CreateGoalCommandHandler> logger) 
    : BaseCommandHandler<CreateGoalCommand, GoalResponse>(logger), ICommandHandler<CreateGoalCommand, GoalResponse>
{
    private readonly IGoalRepository _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
    private readonly IValidator<CreateGoalRequest> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    public override async Task<GoalResponse> Execute(CreateGoalCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await _validator.ValidateAndThrowAsync(request, token);

        var entity = request.ToEntity();

        var result = await _goalRepository.CreateAsync(entity, token);

        return result.ToResponse();
    }
}
