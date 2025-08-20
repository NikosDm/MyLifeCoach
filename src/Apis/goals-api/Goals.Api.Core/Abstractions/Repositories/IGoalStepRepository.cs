using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Goals.Api.Domain.Entities;

namespace Goals.Api.Core.Abstractions.Repositories;

public interface IGoalStepRepository
{
    Task<IReadOnlyList<GoalStep>> GetAsync(CancellationToken token = default);
    Task<GoalStep> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IReadOnlyList<GoalStep>> SearchAsync(Expression<Func<GoalStep, bool>> options = null, CancellationToken token = default);
    Task<GoalStep> CreateAsync(GoalStep entity, CancellationToken token = default);
    Task<GoalStep> UpdateAsync(Guid id, GoalStep entity, CancellationToken token = default);
}
