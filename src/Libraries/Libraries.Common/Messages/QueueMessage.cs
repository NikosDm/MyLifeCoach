using System;

namespace Libraries.Common.Messages;

public class QueueMessage
{
    public string MessageType { get; set; }
    public string MessageName { get; set; }
    public string Data { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public QueueMessage() { }

    public QueueMessage(string messageName, string messageType, string data)
    {
        MessageName = messageName ?? throw new ArgumentNullException(nameof(messageName));
        MessageType = messageType ?? throw new ArgumentNullException(nameof(messageType));
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }
}