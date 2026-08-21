using System;

using Libraries.Common.Abstractions;

namespace Libraries.Common.Messages;

public class UserActivationChangedMessage : IMessage
{
    public string EntityType => "User";
    public string Action => "ActivationStatusChanged";
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset? DeactivationDate { get; set; }
}