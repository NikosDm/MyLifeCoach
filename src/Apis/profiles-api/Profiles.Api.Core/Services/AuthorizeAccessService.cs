using System;

using Libraries.Common.Abstractions;
using Libraries.Common.Constants;

using Profiles.Api.Core.Abstractions.Services;

namespace Profiles.Api.Core.Services;

public class AuthorizeAccessService(IUserContext userContext)
    : IAuthorizeAccessService
{
    private readonly IUserContext _userContext = userContext
        ?? throw new ArgumentNullException(nameof(userContext));

    public bool CanUserAccessProfileAction(Guid userId)
    {
        return (_userContext.Role == SecurityConstants.ADMIN_ROLE
            || _userContext.UserId == userId)
            && _userContext.IsActive;
    }
}