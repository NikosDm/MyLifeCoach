using System;
using System.Threading;
using System.Threading.Tasks;

using Profiles.Api.Core.Dtos.Users.Responses;

namespace Profiles.Api.Core.Abstractions;

public interface IUserStatusPublisher
{
    Task<UserResponse> PublishUserStatusAsync(Guid userId, bool setActive, CancellationToken token = default);
}