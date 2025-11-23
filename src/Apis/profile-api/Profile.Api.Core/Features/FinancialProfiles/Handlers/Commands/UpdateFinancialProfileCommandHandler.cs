using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profile.Api.Core.Abstractions;
using Profile.Api.Core.Dtos.FinancialProfiles.Requests;
using Profile.Api.Core.Dtos.FinancialProfiles.Responses;
using Profile.Api.Core.Extensions;
using Profile.Api.Core.Features.FinancialProfiles.Requests.Commands;
using Profile.Api.Domain.Enums;
using Profile.Api.Domain.Models;

namespace Profile.Api.Core.Features.FinancialProfiles.Handlers.Commands;

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