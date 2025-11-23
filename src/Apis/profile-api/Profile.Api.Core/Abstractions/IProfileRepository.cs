using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Profile.Api.Domain.Abstractions;
using Profile.Api.Domain.Enums;

namespace Profile.Api.Core.Abstractions;

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
    Task<T> CreateAsync(T profile, CancellationToken token = default);
    Task<IReadOnlyList<T>> SearchAsync(Expression<Func<T, bool>> options = null, CancellationToken token = default);
    Task<T> UpdateAsync(T profile, CancellationToken token = default);
}
