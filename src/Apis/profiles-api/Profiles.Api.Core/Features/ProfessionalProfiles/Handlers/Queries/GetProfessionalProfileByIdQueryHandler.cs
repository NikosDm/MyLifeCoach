using System;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.Core.Dtos.ProfessionalProfiles.Responses;
using Profiles.Api.Core.Extensions;
using Profiles.Api.Core.Features.ProfessionalProfiles.Requests.Queries;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.Core.Features.ProfessionalProfiles.Handlers.Queries;

internal sealed class GetProfessionalProfileByIdQueryHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    ILogger<GetProfessionalProfileByIdQueryHandler> logger)
    : BaseQueryHandler<GetProfessionalProfileByIdQuery, ProfessionalProfileResponse>(logger),
    IQueryHandler<GetProfessionalProfileByIdQuery, ProfessionalProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new ArgumentNullException(nameof(profileRepositoryFactory));

    public override async Task<ProfessionalProfileResponse> Execute(GetProfessionalProfileByIdQuery query, CancellationToken token = default)
    {
        var repository = _profileRepositoryFactory.Get<ProfessionalProfile>(ProfileType.PROFESSIONAL);

        var result = await repository.GetByIdAsync(query.Id, token);

        return result.ToResponse();
    }
}
