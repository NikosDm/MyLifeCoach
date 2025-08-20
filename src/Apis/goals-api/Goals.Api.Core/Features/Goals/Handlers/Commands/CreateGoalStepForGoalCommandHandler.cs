using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.GoalSteps.Requests;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.Goals.Requests.Commands;
using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace Goals.Api.Core.Features.Goals.Handlers.Commands;

public sealed class CreateGoalStepForGoalCommandHandler(
    IGoalStepRepository goalStepRepository,
    IValidator<CreateStepForGoalRequest> validator,
    ILogger<CreateGoalStepForGoalCommandHandler> logger) 
    : BaseCommandHandler<CreateGoalStepForGoalCommand, GoalStepResponse>(logger), ICommandHandler<CreateGoalStepForGoalCommand, GoalStepResponse>
{
    private readonly IGoalStepRepository _goalStepRepository = goalStepRepository ?? throw new ArgumentNullException(nameof(goalStepRepository));
    private readonly IValidator<CreateStepForGoalRequest> _validator = validator ?? throw new ArgumentNullException(nameof(goalStepRepository));

    public override async Task<GoalStepResponse> Execute(CreateGoalStepForGoalCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await _validator.ValidateAndThrowAsync(request, token);

        var entity = request.ToEntity();

        var result = await _goalStepRepository.CreateAsync(entity, token);

        return result.ToResponse();
    }
}
