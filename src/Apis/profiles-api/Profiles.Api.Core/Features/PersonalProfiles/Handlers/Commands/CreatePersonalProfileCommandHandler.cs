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

internal sealed class CreatePersonalProfileCommandHandler(
    IProfileRepositoryFactory profileRepositoryFactory,
    IValidator<CreatePersonalProfileRequest> validator,
    ILogger<CreatePersonalProfileCommandHandler> logger)
: BaseCommandHandler<CreatePersonalProfileCommand, PersonalProfileResponse>(logger), ICommandHandler<CreatePersonalProfileCommand, PersonalProfileResponse>
{
    public override async Task<PersonalProfileResponse> Execute(CreatePersonalProfileCommand command, CancellationToken token = default)
    {
        var request = command.Request;
        await validator.ValidateAndThrowAsync(request, token);

        var repository = profileRepositoryFactory.Get<PersonalProfile>(ProfileType.PERSONAL);

        var entity = request.ToEntity();
        var result = await repository.CreateAsync(entity, token);

        return result.ToResponse();
    }
}
