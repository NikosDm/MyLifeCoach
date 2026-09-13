using System;

using Libraries.Common.Abstractions;

namespace Libraries.Common.Messages;

public class UserRegisteredMessage : IMessage
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string EntityType => "User";
    public string Action => "Registered";
}