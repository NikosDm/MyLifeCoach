using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

using DotNetCore.CAP;

using Libraries.Common.Messages;
using Libraries.DataInfrastructure.Abstractions;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Libraries.DataInfrastructure.Messages;

public class MessageDispatcher(ICapPublisher capPublisher) : IMessageDispatcher
{
    private readonly ICapPublisher _capPublisher = capPublisher
        ?? throw new ArgumentNullException(nameof(capPublisher));

    /// <summary>
    /// Do not immidiately 'await' this method to allow for transaction scope to work correctly.
    /// By returning the Task directly, it allows the caller controls when and where to await, preserving the transaction scope
    /// </summary>
    /// <param name="dbConnection"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public ValueTask<ICapTransaction> BeginTransactionAsync(IDbConnection dbConnection, CancellationToken token = default)
        => dbConnection.BeginTransactionAsync(_capPublisher, autoCommit: false, cancellationToken: token);

    /// <summary>
    /// Do not immidiately 'await' this method to allow for transaction scope to work correctly.
    /// By returning the Task directly, it allows the caller controls when and where to await, preserving the transaction scope
    /// </summary>
    /// <param name="databaseFacade"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<IDbContextTransaction> BeginTransactionAsync(DatabaseFacade databaseFacade, CancellationToken token = default)
        => databaseFacade.BeginTransactionAsync(_capPublisher, autoCommit: false, cancellationToken: token);

    /// <summary>
    /// Dispatches a message to the message bus
    /// </summary>
    /// <param name="message"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task DispachAsync(IMessage message, CancellationToken token = default)
    {
        if (message is null)
        {
            return;
        }

        await _capPublisher.PublishAsync(message.GetType().Name, message, cancellationToken: token);
    }
}