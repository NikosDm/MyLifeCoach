using System;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.Core.Dtos.PersonalProfiles.Responses;
using Profiles.Api.Core.Extensions;
using Profiles.Api.Core.Features.PersonalProfiles.Requests.Queries;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.Core.Features.PersonalProfiles.Handlers.Queries;

internal sealed class GetPersonalProfileByIdQueryHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    ILogger<GetPersonalProfileByIdQueryHandler> logger)
    : BaseQueryHandler<GetPersonalProfileByIdQuery, PersonalProfileResponse>(logger),
    IQueryHandler<GetPersonalProfileByIdQuery, PersonalProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new ArgumentNullException(nameof(profileRepositoryFactory));

    public override async Task<PersonalProfileResponse> Execute(GetPersonalProfileByIdQuery query, CancellationToken token = default)
    {
        var repository = _profileRepositoryFactory.Get<PersonalProfile>(ProfileType.PERSONAL);

        var result = await repository.GetByIdAsync(query.Id, token);

        return result.ToResponse();
    }
}
