using System;
using System.Threading;
using System.Threading.Tasks;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.GoalSteps.Requests.Queries;
using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace Goals.Api.Core.Features.GoalSteps.Handlers.Queries;

public sealed class GetGoalStepByIdQueryHandler(
    IGoalStepRepository goalStepRepository,
    ILogger<GetGoalStepByIdQueryHandler> logger) 
    : BaseQueryHandler<GetGoalStepByIdQuery, GoalStepResponse>(logger),
    IQueryHandler<GetGoalStepByIdQuery, GoalStepResponse>
{
    private readonly IGoalStepRepository _goalStepRepository = goalStepRepository ?? throw new ArgumentNullException(nameof(goalStepRepository));

    public override async Task<GoalStepResponse> Execute(GetGoalStepByIdQuery query, CancellationToken token = default)
    {
        var result = await _goalStepRepository.GetByIdAsync(query.Id, token);
        return result.ToResponse();
    }
}
