using System;

using Libraries.Common.Abstractions;

namespace Libraries.Api.Context;

public sealed class UserContext(IUserContextAccessor accessor) : IUserContext
{
    private readonly IUserContextAccessor _accessor = accessor
        ?? throw new ArgumentNullException(nameof(accessor));

    public bool IsAuthenticated => _accessor.Current?.IsAuthenticated ?? false;
    public Guid? UserId => _accessor.Current?.UserId;
    public string Username => _accessor.Current?.Username ?? string.Empty;
    public string Role => _accessor.Current?.Role ?? string.Empty;
}

