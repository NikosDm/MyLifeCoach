using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Goals.Api.Domain.Entities;

namespace Goals.Api.Core.Abstractions.Repositories;

public interface IGoalTypeRepository
{
    Task<IReadOnlyList<GoalType>> GetAsync(CancellationToken token = default);
    Task<GoalType> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IReadOnlyList<GoalType>> SearchAsync(Expression<Func<GoalType, bool>> options = null, CancellationToken token = default);
    Task<GoalType> CreateAsync(GoalType entity, CancellationToken token = default);
    Task<GoalType> UpdateAsync(GoalType entity, CancellationToken token = default);
}
