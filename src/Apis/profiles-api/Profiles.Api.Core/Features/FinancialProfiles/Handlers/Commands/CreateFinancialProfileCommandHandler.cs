using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.Core.Dtos.FinancialProfiles.Requests;
using Profiles.Api.Core.Dtos.FinancialProfiles.Responses;
using Profiles.Api.Core.Extensions;
using Profiles.Api.Core.Features.FinancialProfiles.Requests.Commands;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.Core.Features.FinancialProfiles.Handlers.Commands;

internal sealed class CreateFinancialProfileCommandHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    IValidator<CreateFinancialProfileRequest> validator,
    ILogger<CreateFinancialProfileCommandHandler> logger)
    : BaseCommandHandler<CreateFinancialProfileCommand, FinancialProfileResponse>(logger),
    ICommandHandler<CreateFinancialProfileCommand, FinancialProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new System.ArgumentNullException(nameof(profileRepositoryFactory));

    private readonly IValidator<CreateFinancialProfileRequest> _validator = validator
        ?? throw new System.ArgumentNullException(nameof(validator));

    public override async Task<FinancialProfileResponse> Execute(CreateFinancialProfileCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await _validator.ValidateAndThrowAsync(request, token);

        var repository = _profileRepositoryFactory.Get<FinancialProfile>(ProfileType.FINANCIAL);

        var entity = request.ToEntity();
        var result = await repository.CreateAsync(entity, token);

        return result.ToResponse();
    }
}