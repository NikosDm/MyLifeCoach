using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profile.Api.Core.Abstractions;
using Profile.Api.Core.Dtos.ProfessionalProfiles.Requests;
using Profile.Api.Core.Dtos.ProfessionalProfiles.Responses;
using Profile.Api.Core.Extensions;
using Profile.Api.Core.Features.ProfessionalProfiles.Requests.Commands;
using Profile.Api.Domain.Enums;
using Profile.Api.Domain.Models;

namespace Profile.Api.Core.Features.ProfessionalProfiles.Handlers.Commands;

internal sealed class CreateProfessionalProfileCommandHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    IValidator<CreateProfessionalProfileRequest> validator,
    ILogger<CreateProfessionalProfileCommandHandler> logger)
    : BaseCommandHandler<CreateProfessionalProfileCommand, ProfessionalProfileResponse>(logger),
    ICommandHandler<CreateProfessionalProfileCommand, ProfessionalProfileResponse>
{
    private readonly IProfileRepositoryFactory _profileRepositoryFactory = profileRepositoryFactory
        ?? throw new System.ArgumentNullException(nameof(profileRepositoryFactory));

    private readonly IValidator<CreateProfessionalProfileRequest> _validator = validator
        ?? throw new System.ArgumentNullException(nameof(validator));

    public override async Task<ProfessionalProfileResponse> Execute(CreateProfessionalProfileCommand command, CancellationToken token = default)
    {
        var request = command.Request;

        await _validator.ValidateAndThrowAsync(request, token);

        var repository = _profileRepositoryFactory.Get<ProfessionalProfile>(ProfileType.PROFESSIONAL);

        var entity = request.ToEntity();
        var result = await repository.CreateAsync(entity, token);

        return result.ToResponse();
    }
}
