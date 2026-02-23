using System.Threading;

using Libraries.Common.Abstractions;
using Libraries.Common.Entities;

namespace Libraries.Api.Context;

public class UserContextAccessor : IUserContextAccessor
{
    // AsyncLocal<T> is a .NET type that stores data that flows with the current async execution context.
    // Since we are using CAP to subscribe to events, and CAP does not use HTTP context and makes use of asynchronous processing like Command Handlers
    // AsyncLocal allows us to store user context data that is specific to the current asynchronous flow.
    private static readonly AsyncLocal<UserContextData> _current = new();
    public UserContextData Current
    {
        get => _current.Value;
        set => _current.Value = value;
    }
}