using Libraries.Common.Messages;

using Profiles.Api.Domain.Models;

namespace Profiles.Api.DataPersistence.Extensions;

public static class UserExtensions
{
    public static UserActivationChangedMessage ToUserActivationChangedMessage(this User user)
    {
        return new UserActivationChangedMessage
        {
            UserId = user.Id,
            Username = user.PersonalProfile?.Payload?.Username,
            IsActive = user.IsActive,
            DeactivationDate = user.DeactivationDate
        };
    }
}
