using System;
using System.Threading;
using System.Threading.Tasks;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.GoalTypes.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.GoalTypes.Requests.Queries;
using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace Goals.Api.Core.Features.GoalTypes.Handlers.Queries;

internal sealed class GetGoalTypeQueryHandler(
    IGoalTypeRepository goalTypeRepository,
    ILogger<GetGoalTypeQueryHandler> logger) 
    : BaseQueryHandler<GetGoalTypeQuery, GoalTypeResponse>(logger),
    IQueryHandler<GetGoalTypeQuery, GoalTypeResponse>
{
    private readonly IGoalTypeRepository _goalTypeRepository = goalTypeRepository ?? throw new ArgumentNullException(nameof(goalTypeRepository));

    public override async Task<GoalTypeResponse> Execute(GetGoalTypeQuery query, CancellationToken token = default)
    {
        var result = await _goalTypeRepository.GetByIdAsync(query.Id, token);

        return result.ToResponse();
    }
}
