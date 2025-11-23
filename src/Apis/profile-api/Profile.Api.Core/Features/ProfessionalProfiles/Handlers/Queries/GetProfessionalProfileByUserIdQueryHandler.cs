using System;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profile.Api.Core.Abstractions;
using Profile.Api.Core.Dtos.ProfessionalProfiles.Responses;
using Profile.Api.Core.Extensions;
using Profile.Api.Core.Features.ProfessionalProfiles.Requests.Queries;
using Profile.Api.Domain.Enums;
using Profile.Api.Domain.Models;

namespace Profile.Api.Core.Features.ProfessionalProfiles.Handlers.Queries;

internal sealed class GetProfessionalProfileByUserIdQueryHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    ILogger<GetProfessionalProfileByUserIdQueryHandler> logger)
    : BaseQueryHandler<GetProfessionalProfileByUserIdQuery, ProfessionalProfileResponse>(logger),
    IQueryHandler<GetProfessionalProfileByUserIdQuery, ProfessionalProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new ArgumentNullException(nameof(profileRepositoryFactory));

    public override async Task<ProfessionalProfileResponse> Execute(GetProfessionalProfileByUserIdQuery query, CancellationToken token = default)
    {
        var repository = _profileRepositoryFactory.Get<ProfessionalProfile>(ProfileType.PROFESSIONAL);

        var result = await repository.GetByUserIdAsync(query.UserId, token);

        return result.ToResponse();
    }
}
