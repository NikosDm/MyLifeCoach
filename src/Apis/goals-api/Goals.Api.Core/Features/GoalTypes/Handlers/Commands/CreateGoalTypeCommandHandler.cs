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

public sealed class CreateGoalTypeCommandHandler(
    IGoalTypeRepository goalTypeRepository,
    IValidator<CreateGoalTypeRequest> validator,
    ILogger<CreateGoalTypeCommandHandler> logger) 
    : BaseCommandHandler<CreateGoalTypeCommand, GoalTypeResponse>(logger), ICommandHandler<CreateGoalTypeCommand, GoalTypeResponse>
{
    private readonly IGoalTypeRepository _goalTypeRepository = goalTypeRepository;
    private readonly IValidator<CreateGoalTypeRequest> _validator = validator;

    public override async Task<GoalTypeResponse> Execute(CreateGoalTypeCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await _validator.ValidateAndThrowAsync(request, token);

        var entity = request.ToEntity();
        var result = await _goalTypeRepository.CreateAsync(entity, token);

        return result.ToResponse();
    }
}
