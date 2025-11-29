using System;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.Core.Dtos.FinancialProfiles.Responses;
using Profiles.Api.Core.Extensions;
using Profiles.Api.Core.Features.FinancialProfiles.Requests.Queries;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.Core.Features.FinancialProfiles.Handlers.Queries;

internal sealed class GetFinancialProfileByIdQueryHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    ILogger<GetFinancialProfileByIdQueryHandler> logger)
    : BaseQueryHandler<GetFinancialProfileByIdQuery, FinancialProfileResponse>(logger),
    IQueryHandler<GetFinancialProfileByIdQuery, FinancialProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new ArgumentNullException(nameof(profileRepositoryFactory));

    public override async Task<FinancialProfileResponse> Execute(GetFinancialProfileByIdQuery query, CancellationToken token = default)
    {
        var repository = _profileRepositoryFactory.Get<FinancialProfile>(ProfileType.FINANCIAL);

        var result = await repository.GetByIdAsync(query.Id, token);

        return result.ToResponse();
    }
}
