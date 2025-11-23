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