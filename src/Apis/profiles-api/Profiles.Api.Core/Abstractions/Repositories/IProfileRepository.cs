using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Profiles.Api.Domain.Abstractions;
using Profiles.Api.Domain.Enums;

namespace Profiles.Api.Core.Abstractions.Repositories;

public interface IProfileRepository
{
    ProfileType Handles { get; }
}

public interface IProfileRepository<T> : IProfileRepository
    where T : ProfileBase
{
    Task<T> GetByUserIdAsync(Guid userId, CancellationToken token = default);
    Task<IReadOnlyList<T>> GetAsync(CancellationToken token = default);
    Task<T> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<T> CreateAsync(T profile, bool saveChanges = true, CancellationToken token = default);
    Task<IReadOnlyList<T>> SearchAsync(Expression<Func<T, bool>> options = null, CancellationToken token = default);
    Task<T> UpdateAsync(T profile, bool saveChanges = true, CancellationToken token = default);
}
