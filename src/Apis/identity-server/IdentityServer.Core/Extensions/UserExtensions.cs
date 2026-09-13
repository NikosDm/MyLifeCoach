using System;

using IdentityServer.Core.Dtos.Responses;
using IdentityServer.DataAccess.Entities;

using Libraries.Common.Messages;

namespace IdentityServer.Core.Extensions;

public static class UserExtensions
{
    public static UserRegisteredMessage ToUserCreatedMessage(this ApplicationUser source, string fullName) =>
        new()
        {
            Id = Guid.Parse(source.Id),
            Username = source.UserName,
            FullName = fullName,
            Email = source.Email,
        };

    public static UserRegisteredMessage ToUserCreatedMessage(this RegisterResponse source, string fullName) =>
        new()
        {
            Id = Guid.Parse(source.User.Id),
            Username = source.User.Username,
            FullName = fullName,
            Email = source.User.Email,
        };
}