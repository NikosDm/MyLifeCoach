using System;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.Core.Dtos.FitnessProfiles.Responses;
using Profiles.Api.Core.Extensions;
using Profiles.Api.Core.Features.FitnessProfiles.Requests.Queries;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.Core.Features.FitnessProfiles.Handlers.Queries;

internal sealed class GetFitnessProfileByUserIdQueryHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    ILogger<GetFitnessProfileByUserIdQueryHandler> logger)
    : BaseQueryHandler<GetFitnessProfileByUserIdQuery, FitnessProfileResponse>(logger),
    IQueryHandler<GetFitnessProfileByUserIdQuery, FitnessProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new ArgumentNullException(nameof(profileRepositoryFactory));

    public override async Task<FitnessProfileResponse> Execute(GetFitnessProfileByUserIdQuery query, CancellationToken token = default)
    {
        var repository = _profileRepositoryFactory.Get<FitnessProfile>(ProfileType.FITNESS);
        var result = await repository.GetByUserIdAsync(query.UserId, token);
        return result.ToResponse();
    }
}
