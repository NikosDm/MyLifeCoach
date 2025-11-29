using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.Core.Dtos.ProfessionalProfiles.Requests;
using Profiles.Api.Core.Dtos.ProfessionalProfiles.Responses;
using Profiles.Api.Core.Extensions;
using Profiles.Api.Core.Features.ProfessionalProfiles.Requests.Commands;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.Core.Features.ProfessionalProfiles.Handlers.Commands;

internal sealed class UpdateProfessionalProfileCommandHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    IValidator<UpdateProfessionalProfileRequest> validator,
    ILogger<UpdateProfessionalProfileCommandHandler> logger)
    : BaseCommandHandler<UpdateProfessionalProfileCommand, ProfessionalProfileResponse>(logger),
    ICommandHandler<UpdateProfessionalProfileCommand, ProfessionalProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new System.ArgumentNullException(nameof(profileRepositoryFactory));
    private readonly IValidator<UpdateProfessionalProfileRequest> _validator = validator
        ?? throw new System.ArgumentNullException(nameof(validator));

    public override async Task<ProfessionalProfileResponse> Execute(UpdateProfessionalProfileCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await _validator.ValidateAndThrowAsync(request, token);

        var repository = _profileRepositoryFactory.Get<ProfessionalProfile>(ProfileType.PROFESSIONAL);
        var profile = await repository.GetByIdAsync(command.Id, token);
        profile = request.MapRequestToEntity(profile);
        var result = await repository.UpdateAsync(profile, token);

        return result.ToResponse();
    }
}
