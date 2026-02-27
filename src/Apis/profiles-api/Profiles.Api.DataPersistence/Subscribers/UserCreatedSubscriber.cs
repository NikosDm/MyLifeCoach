using System;
using System.Threading.Tasks;

using DotNetCore.CAP;

using Libraries.Common.Messages;
using Libraries.Common.Extensions;

using Microsoft.Extensions.Logging;
using Profiles.Api.Core.Extensions;
using Profiles.Api.Core.Features.PersonalProfiles.Requests.Commands;
using Libraries.Common.Abstractions.Commands;
using Profiles.Api.Core.Dtos.PersonalProfiles.Responses;

namespace Profiles.Api.DataPersistence.Subscribers;

public class UserCreatedSubscriber(
    ILogger<UserCreatedSubscriber> logger,
    ICommandHandler<CreatePersonalProfileCommand, PersonalProfileResponse> commandHandler) : ICapSubscribe
{
    private readonly ILogger<UserCreatedSubscriber> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));
    private readonly ICommandHandler<CreatePersonalProfileCommand, PersonalProfileResponse> _commandHandler = commandHandler
        ?? throw new ArgumentNullException(nameof(commandHandler));

    [CapSubscribe("User.Created", Group = "profiles-api")]
    public async Task HandleUserCreatedEvent(QueueMessage message)
    {
        _logger.LogInformation("Received User.Created event: {Message}", message.MessageName);

        var userCreatedMessage = message.ToMessage<UserCreatedMessage>(message.MessageType);
        var createProfileRequest = userCreatedMessage.ToCreateRequest();

        await _commandHandler.HandleAsync(new CreatePersonalProfileCommand(createProfileRequest));

        _logger.LogInformation("Successfully processed User.Created event for UserId: {UserId}", userCreatedMessage.Id);
    }
}