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

internal sealed class UpdateFinancialProfileCommandHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    IValidator<UpdateFinancialProfileRequest> validator,
    ILogger<UpdateFinancialProfileCommandHandler> logger)
    : BaseCommandHandler<UpdateFinancialProfileCommand, FinancialProfileResponse>(logger),
    ICommandHandler<UpdateFinancialProfileCommand, FinancialProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new System.ArgumentNullException(nameof(profileRepositoryFactory));
    private readonly IValidator<UpdateFinancialProfileRequest> _validator = validator
        ?? throw new System.ArgumentNullException(nameof(validator));

    public override async Task<FinancialProfileResponse> Execute(UpdateFinancialProfileCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await _validator.ValidateAndThrowAsync(request, token);

        var repository = profileRepositoryFactory.Get<FinancialProfile>(ProfileType.FINANCIAL);
        var profile = await repository.GetByIdAsync(command.Id, token);
        profile = request.MapRequestToEntity(profile);
        var result = await repository.UpdateAsync(profile, token);

        return result.ToResponse();
    }
}