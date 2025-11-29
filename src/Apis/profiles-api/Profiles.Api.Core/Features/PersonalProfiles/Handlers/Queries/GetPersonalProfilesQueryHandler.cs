using System.Collections.Generic;
using System.Linq;
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