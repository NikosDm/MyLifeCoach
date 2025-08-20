using System;
using System.Threading;
using System.Threading.Tasks;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.Core.Dtos.Goals.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.Goals.Requests.Queries;
using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace Goals.Api.Core.Features.Goals.Handlers.Queries;

public sealed class GetGoalByIdQueryHandler(
    IGoalRepository goalRepository,
    ILogger<GetGoalByIdQueryHandler> logger) 
    : BaseQueryHandler<GetGoalByIdQuery, GoalResponse>(logger),
    IQueryHandler<GetGoalByIdQuery, GoalResponse>
{
    private readonly IGoalRepository _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));

    public override async Task<GoalResponse> Execute(GetGoalByIdQuery query, CancellationToken token = default)
    {
        var result = await _goalRepository.GetByIdAsync(query.Id, token);
        return result.ToResponse();
    }
}
