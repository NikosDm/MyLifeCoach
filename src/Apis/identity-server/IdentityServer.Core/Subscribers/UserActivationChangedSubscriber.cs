using System;
using System.Linq;
using System.Threading.Tasks;

using DotNetCore.CAP;

using Libraries.Common.Messages;
using Libraries.Common.Extensions;

using Microsoft.Extensions.Logging;

using IdentityServer.Core.Abstractions;

namespace IdentityServer.Core.Subscribers;

public class UserActivationChangedSubscriber(
    ILogger<UserActivationChangedSubscriber> logger,
    IAccountService accountService) : ICapSubscribe
{
    private readonly ILogger<UserActivationChangedSubscriber> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));
    private readonly IAccountService _accountService = accountService
        ?? throw new ArgumentNullException(nameof(accountService));

    [CapSubscribe("User.ActivationStatusChanged", Group = "identity-server")]
    public async Task HandleUserActivationChangedEvent(QueueMessage message)
    {
        _logger.LogInformation("Received User.ActivationStatusChanged event: {Message}", message.MessageName);

        try
        {
            var activationMessage = message.ToMessage<UserActivationChangedMessage>(message.MessageType);

            var result = await _accountService.ChangeUserStatusAsync(
                activationMessage.UserId,
                activationMessage.IsActive,
                activationMessage.DeactivationDate);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to update user {activationMessage.UserId}: {string.Join(';', result.Errors.Select(e => e.Description))}");
            }
            else
            {
                _logger.LogInformation("Successfully updated activation status for user {UserId}", activationMessage.UserId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing User.ActivationStatusChanged event: {Message}", message.MessageName);
            throw; // Rethrow the exception to ensure the message is retried
        }
    }
}
