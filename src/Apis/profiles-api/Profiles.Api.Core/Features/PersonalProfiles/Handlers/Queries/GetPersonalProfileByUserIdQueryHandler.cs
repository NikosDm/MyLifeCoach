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

internal sealed class GetPersonalProfileByUserIdQueryHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    ILogger<GetPersonalProfileByUserIdQueryHandler> logger)
    : BaseQueryHandler<GetPersonalProfileByUserIdQuery, PersonalProfileResponse>(logger),
    IQueryHandler<GetPersonalProfileByUserIdQuery, PersonalProfileResponse>
{
    public override async Task<PersonalProfileResponse> Execute(GetPersonalProfileByUserIdQuery query, CancellationToken token = default)
    {
        var repository = profileRepositoryFactory.Get<PersonalProfile>(ProfileType.PERSONAL);

        var result = await repository.GetByUserIdAsync(query.UserId, token);
        return result.ToResponse();
    }
}