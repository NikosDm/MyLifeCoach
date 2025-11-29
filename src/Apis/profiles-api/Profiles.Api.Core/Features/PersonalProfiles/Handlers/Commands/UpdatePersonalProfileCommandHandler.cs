using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.Core.Dtos.PersonalProfiles.Requests;
using Profiles.Api.Core.Dtos.PersonalProfiles.Responses;
using Profiles.Api.Core.Extensions;
using Profiles.Api.Core.Features.PersonalProfiles.Requests.Commands;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.Core.Features.PersonalProfiles.Handlers.Commands;

internal sealed class UpdatePersonalProfileCommandHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    IValidator<UpdatePersonalProfileRequest> validator,
    ILogger<UpdatePersonalProfileCommandHandler> logger)
: BaseCommandHandler<UpdatePersonalProfileCommand, PersonalProfileResponse>(logger), ICommandHandler<UpdatePersonalProfileCommand, PersonalProfileResponse>
{
    public override async Task<PersonalProfileResponse> Execute(UpdatePersonalProfileCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await validator.ValidateAndThrowAsync(request, token);

        var repository = profileRepositoryFactory.Get<PersonalProfile>(ProfileType.PERSONAL);

        var profile = await repository.GetByIdAsync(command.Id, token);

        profile = request.MapRequestToEntity(profile);

        var result = await repository.UpdateAsync(profile, token);

        return result.ToResponse();
    }
}