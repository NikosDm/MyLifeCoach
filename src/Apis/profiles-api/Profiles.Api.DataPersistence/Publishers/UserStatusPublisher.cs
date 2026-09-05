using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Libraries.Common.Constants;
using Libraries.DataInfrastructure.Abstractions;

using Profiles.Api.Core.Abstractions.Publishers;
using Profiles.Api.DataPersistence.Context;
using Profiles.Api.DataPersistence.Extensions;
using Profiles.Api.Domain.Models;

using Microsoft.EntityFrameworkCore;
using Profiles.Api.Core.Dtos.Users.Responses;

namespace Profiles.Api.DataPersistence.Publishers;

public class UserStatusPublisher(
    ProfileDbContext dbContext,
    IMessageDispatcher messageDispatcher) : IUserStatusPublisher
{
    private readonly ProfileDbContext _dbContext = dbContext
        ?? throw new ArgumentNullException(nameof(dbContext));

    private readonly IMessageDispatcher _messageDispatcher = messageDispatcher
        ?? throw new ArgumentNullException(nameof(messageDispatcher));

    public async Task<UserResponse> PublishUserStatusAsync(
        Guid userId,
        bool setActive,
        CancellationToken token = default)
    {
        await using var transaction = await _messageDispatcher.BeginTransactionAsync(_dbContext.Database, token);

        try
        {
            var user = await _dbContext.Set<User>()
                .Include(u => u.PersonalProfile)
                .SingleOrDefaultAsync(u => u.Id == userId, token)
            ?? throw new InvalidOperationException($"User with ID {userId} not found.");

            user.IsActive = setActive;
            user.DeactivationDate = setActive ? null : DateTimeOffset.UtcNow;

            var message = user.ToUserActivationChangedMessage();

            var headers = new Dictionary<string, string>
            {
                { MessageHeaderConstants.UserId, user.Id.ToString() },
                { MessageHeaderConstants.Username, message.Username }
            };

            await _messageDispatcher.DispatchAsync(message, headers, token);

            await _dbContext.SaveChangesAsync(token);

            await transaction.CommitAsync(token);

            return new UserResponse(user.Id, user.Role, user.IsActive);
        }
        catch
        {
            await transaction.RollbackAsync(token);
            throw;
        }
    }
}