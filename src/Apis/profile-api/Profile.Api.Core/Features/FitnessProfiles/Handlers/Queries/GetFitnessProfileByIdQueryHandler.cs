using System;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profile.Api.Core.Abstractions;
using Profile.Api.Core.Dtos.FitnessProfiles.Responses;
using Profile.Api.Core.Extensions;
using Profile.Api.Core.Features.FitnessProfiles.Requests.Queries;
using Profile.Api.Domain.Enums;
using Profile.Api.Domain.Models;

namespace Profile.Api.Core.Features.FitnessProfiles.Handlers.Queries;

internal sealed class GetFitnessProfileByIdQueryHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    ILogger<GetFitnessProfileByIdQueryHandler> logger)
    : BaseQueryHandler<GetFitnessProfileByIdQuery, FitnessProfileResponse>(logger),
    IQueryHandler<GetFitnessProfileByIdQuery, FitnessProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new ArgumentNullException(nameof(profileRepositoryFactory));

    public override async Task<FitnessProfileResponse> Execute(GetFitnessProfileByIdQuery query, CancellationToken token = default)
    {
        var repository = _profileRepositoryFactory.Get<FitnessProfile>(ProfileType.FITNESS);

        var result = await repository.GetByIdAsync(query.Id, token);

        return result.ToResponse();
    }
}
