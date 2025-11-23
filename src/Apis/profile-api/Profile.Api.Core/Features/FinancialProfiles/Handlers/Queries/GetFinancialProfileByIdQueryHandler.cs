using System;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profile.Api.Core.Abstractions;
using Profile.Api.Core.Dtos.FinancialProfiles.Responses;
using Profile.Api.Core.Extensions;
using Profile.Api.Core.Features.FinancialProfiles.Requests.Queries;
using Profile.Api.Domain.Enums;
using Profile.Api.Domain.Models;

namespace Profile.Api.Core.Features.FinancialProfiles.Handlers.Queries;

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
