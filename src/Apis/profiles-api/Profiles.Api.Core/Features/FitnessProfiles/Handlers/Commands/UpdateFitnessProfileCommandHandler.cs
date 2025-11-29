using System;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.Core.Dtos.FitnessProfiles.Requests;
using Profiles.Api.Core.Dtos.FitnessProfiles.Responses;
using Profiles.Api.Core.Extensions;
using Profiles.Api.Core.Features.FitnessProfiles.Requests.Commands;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.Core.Features.FitnessProfiles.Handlers.Commands;

internal sealed class UpdateFitnessProfileCommandHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    IValidator<UpdateFitnessProfileRequest> validator,
    ILogger<UpdateFitnessProfileCommandHandler> logger)
    : BaseCommandHandler<UpdateFitnessProfileCommand, FitnessProfileResponse>(logger),
    ICommandHandler<UpdateFitnessProfileCommand, FitnessProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new ArgumentNullException(nameof(profileRepositoryFactory));
    private readonly IValidator<UpdateFitnessProfileRequest> _validator = validator
        ?? throw new ArgumentNullException(nameof(validator));

    public override async Task<FitnessProfileResponse> Execute(UpdateFitnessProfileCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await _validator.ValidateAndThrowAsync(request, token);

        var repository = _profileRepositoryFactory.Get<FitnessProfile>(ProfileType.FITNESS);
        var entity = await repository.GetByIdAsync(command.Id, token);
        entity = request.MapRequestToEntity(entity);

        var result = await repository.UpdateAsync(entity, token);
        return result.ToResponse();
    }
}
