using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Goals.Api.Domain.Entities;

namespace Goals.Api.Core.Abstractions.Repositories;

public interface IGoalRepository
{
    Task<IReadOnlyList<Goal>> GetAsync(CancellationToken token = default);
    Task<Goal> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<Goal> CreateAsync(Goal goal, CancellationToken token = default);
    Task<IReadOnlyList<Goal>> SearchAsync(Expression<Func<Goal, bool>> options = null, CancellationToken token = default);
    Task<Goal> UpdateAsync(Goal goal, CancellationToken token = default);
}
