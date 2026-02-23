using System.Threading;

using Libraries.Common.Abstractions;
using Libraries.Common.Entities;

namespace Libraries.Api.Context;

public class UserContextAccessor : IUserContextAccessor
{
    private static readonly AsyncLocal<UserContextData> _current = new();
    public UserContextData Current
    {
        get => _current.Value;
        set => _current.Value = value;
    }
}