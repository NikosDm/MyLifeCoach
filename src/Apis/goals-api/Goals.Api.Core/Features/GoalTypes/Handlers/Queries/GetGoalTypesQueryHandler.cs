using System;
using System.Collections.Generic;
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

public sealed class GetGoalTypesQueryHandler(
    IGoalTypeRepository goalTypeRepository,
    ILogger<GetGoalTypesQueryHandler> logger) 
    : BaseQueryHandler<GetGoalTypesQuery, IReadOnlyList<GoalTypeResponse>>(logger),
    IQueryHandler<GetGoalTypesQuery, IReadOnlyList<GoalTypeResponse>>
{
    private readonly IGoalTypeRepository _goalTypeRepository = goalTypeRepository ?? throw new ArgumentNullException(nameof(goalTypeRepository));

    public override async Task<IReadOnlyList<GoalTypeResponse>> Execute(GetGoalTypesQuery query, CancellationToken token = default)
    {
        var result = await _goalTypeRepository.GetAsync(token);

        return [.. result.ToResponse()];
    }
}
