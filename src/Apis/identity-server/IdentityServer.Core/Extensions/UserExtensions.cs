using System;

using IdentityServer.DataAccess.Entities;

using Libraries.Common.Messages;

namespace IdentityServer.Core.Extensions;

public static class UserExtensions
{
    public static UserCreatedMessage ToUserCreatedMessage(this ApplicationUser user, string fullName) =>
        new()
        {
            Id = Guid.Parse(user.Id),
            Username = user.UserName,
            FullName = fullName,
            Email = user.Email,
        };
}