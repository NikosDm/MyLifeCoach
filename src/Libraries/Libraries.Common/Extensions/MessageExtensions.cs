using System;
using System.Text.Json;

using Libraries.Common.Abstractions;
using Libraries.Common.Messages;

namespace Libraries.Common.Extensions;

public static class MessageExtensions
{
    public static T ToMessage<T>(this QueueMessage queueMessage, string messageType)
        where T : class, IMessage
    {
        ArgumentNullException.ThrowIfNull(queueMessage);

        return queueMessage.MessageType != messageType
            ? throw new InvalidOperationException($"Cannot convert message of type {queueMessage.MessageType} to {messageType}")
            : string.IsNullOrEmpty(queueMessage.Data)
            ? throw new InvalidOperationException("Message data is null or empty")
            : JsonSerializer.Deserialize<T>(queueMessage.Data);
    }
}