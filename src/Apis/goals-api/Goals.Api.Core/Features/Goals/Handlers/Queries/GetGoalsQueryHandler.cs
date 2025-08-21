using System;
using System.Collections.Generic;
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

internal sealed class GetGoalsQueryHandler(
    IGoalRepository goalRepository,
    ILogger<GetGoalsQueryHandler> logger) 
    : BaseQueryHandler<GetGoalsQuery, IReadOnlyList<GoalResponse>>(logger),
    IQueryHandler<GetGoalsQuery, IReadOnlyList<GoalResponse>>
{
    private readonly IGoalRepository _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));

    public override async Task<IReadOnlyList<GoalResponse>> Execute(GetGoalsQuery query, CancellationToken token = default)
    {
        var result = await _goalRepository.GetAsync(token);

        return [.. result.ToResponse()];
    }
}
