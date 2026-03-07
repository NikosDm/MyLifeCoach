using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Profiles.Api.Domain.Models;

namespace Profiles.Api.Core.Abstractions;

public interface IUserRepository
{
    Task<IReadOnlyList<User>> GetAsync(CancellationToken token = default);
    Task<User> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<User> CreateAsync(User user, bool saveChanges = true, CancellationToken token = default);
}