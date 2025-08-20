using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.GoalSteps.Requests;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.GoalSteps.Requests.Commands;
using Libraries.Common.Abstractions.Commands;
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

        var entity = request.ToEntity();

        var result = await _goalStepRepository.UpdateAsync(command.Id, entity, token);

        return result.ToResponse();
    }
}
