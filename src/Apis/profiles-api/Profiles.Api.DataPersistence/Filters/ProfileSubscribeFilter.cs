using System;
using System.Threading.Tasks;

using DotNetCore.CAP.Filter;

using Libraries.Common.Abstractions;
using Libraries.Common.Constants;
using Libraries.Common.Entities;

namespace Profiles.Api.DataPersistence.Filters;

public class ProfileSubscribeFilter(IUserContextAccessor accessor) : SubscribeFilter
{
    private readonly IUserContextAccessor _accessor = accessor
        ?? throw new ArgumentNullException(nameof(accessor));

    public override Task OnSubscribeExecutingAsync(ExecutingContext context)
    {
        var headers = context.DeliverMessage.Headers;

        Guid? userId = Guid.TryParse(headers.TryGetValue(MessageHeaderConstants.UserId, out var headerId) ? headerId : null, out var id) ? id : null;

        _accessor.Current = new UserContextData(
            IsAuthenticated: userId is not null,
            UserId: userId,
            Username: headers.TryGetValue(MessageHeaderConstants.Username, out var username) ? username : null,
            Role: headers.TryGetValue(MessageHeaderConstants.Role, out var role) ? role : null
        );

        return base.OnSubscribeExecutingAsync(context);
    }
}

