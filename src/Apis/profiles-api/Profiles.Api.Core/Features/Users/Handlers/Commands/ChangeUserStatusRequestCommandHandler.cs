using System;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Handlers;

using Microsoft.Extensions.Logging;

using Profiles.Api.Core.Abstractions.Publishers;
using Profiles.Api.Core.Dtos.Users.Responses;
using Profiles.Api.Core.Features.Users.Requests.Commands;

namespace Profiles.Api.Core.Features.Users.Handlers.Commands;

internal sealed class ChangeUserStatusRequestCommandHandler(
    IUserStatusPublisher userStatusPublisher,
    ILogger<ChangeUserStatusRequestCommandHandler> logger)
    : BaseCommandHandler<ChangeUserStatusRequestCommand, UserResponse>(logger), ICommandHandler<ChangeUserStatusRequestCommand, UserResponse>
{
    private readonly IUserStatusPublisher _userStatusPublisher = userStatusPublisher
        ?? throw new ArgumentNullException(nameof(userStatusPublisher));

    public override async Task<UserResponse> ExecuteAsync(ChangeUserStatusRequestCommand command, CancellationToken token = default)
    {
        return await _userStatusPublisher.PublishUserStatusAsync(command.UserId, command.Request.SetActive, token);
    }
}