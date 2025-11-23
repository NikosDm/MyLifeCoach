using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profile.Api.Core.Abstractions;
using Profile.Api.Core.Dtos.PersonalProfiles.Responses;
using Profile.Api.Core.Extensions;
using Profile.Api.Core.Features.PersonalProfiles.Requests.Queries;
using Profile.Api.Domain.Enums;
using Profile.Api.Domain.Models;

namespace Profile.Api.Core.Features.PersonalProfiles.Handlers.Queries;

internal sealed class GetPersonalProfilesQueryHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    ILogger<GetPersonalProfilesQueryHandler> logger)
    : BaseQueryHandler<GetPersonalProfilesQuery, IReadOnlyList<PersonalProfileResponse>>(logger),
    IQueryHandler<GetPersonalProfilesQuery, IReadOnlyList<PersonalProfileResponse>>
{
    public override async Task<IReadOnlyList<PersonalProfileResponse>> Execute(GetPersonalProfilesQuery query, CancellationToken token = default)
    {
        var repository = profileRepositoryFactory.Get<PersonalProfile>(ProfileType.PERSONAL);

        var result = await repository.GetAsync(token);

        return [.. result.Select(x => x.ToResponse())];
    }
}