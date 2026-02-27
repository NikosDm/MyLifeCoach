using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using DotNetCore.CAP;

using Libraries.Common.Abstractions;
using Libraries.Common.Constants;
using Libraries.Common.Messages;
using Libraries.DataInfrastructure.Abstractions;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Libraries.DataInfrastructure.Messages;

public class MessageDispatcher(ICapPublisher capPublisher, IUserContext userContext) : IMessageDispatcher
{
    private readonly ICapPublisher _capPublisher = capPublisher
        ?? throw new ArgumentNullException(nameof(capPublisher));

    private readonly IUserContext _userContext = userContext
        ?? throw new ArgumentNullException(nameof(userContext));

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = false,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

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
    /// Dispatches a message to the message bus. It contains additional headers that may be used. 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="token"></param>
    /// <param name="additionalHeaders"></param>
    /// <returns></returns>
    public async Task DispachAsync(IMessage message, Dictionary<string, string> additionalHeaders = null, CancellationToken token = default)
    {
        if (message is null)
        {
            return;
        }

        var jsonData = JsonSerializer.Serialize(message, message.GetType(), _jsonSerializerOptions);
        var wrappedMessage = new QueueMessage(
            message.GetMessageName(),
            message.GetType().FullName,
            jsonData);

        var headers = BuildHeaders(additionalHeaders);

        await _capPublisher.PublishAsync(message.GetMessageName(), wrappedMessage, headers, token);
    }

    private Dictionary<string, string> BuildHeaders(Dictionary<string, string> additionalHeaders = null)
    {
        var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [MessageHeaderConstants.Producer] = AppDomain.CurrentDomain.FriendlyName,
            [MessageHeaderConstants.CorrelationId] = Activity.Current?.TraceId.ToString()
                ?? Guid.NewGuid().ToString("N")
        };

        if (_userContext is not null && _userContext.IsAuthenticated)
        {
            headers[MessageHeaderConstants.UserId] = _userContext.UserId.HasValue ?
                _userContext.UserId.Value.ToString() : string.Empty;

            if (!string.IsNullOrWhiteSpace(_userContext.Username))
                headers[MessageHeaderConstants.Username] = _userContext.Username;

            if (!string.IsNullOrWhiteSpace(_userContext.Role))
                headers[MessageHeaderConstants.Role] = _userContext.Role;
        }

        if (additionalHeaders is not null)
        {
            foreach (var kvp in additionalHeaders)
            {
                headers[kvp.Key] = kvp.Value;
            }
        }

        return headers;
    }
}
