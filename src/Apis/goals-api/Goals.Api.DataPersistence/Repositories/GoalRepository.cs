using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.DataPersistence.Context;
using Goals.Api.Domain.Entities;
using Libraries.Common.Abstractions;
using Libraries.DataInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Goals.Api.DataPersistence.Repositories;

internal sealed class GoalRepository(GoalsDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<Goal, GoalsDbContext>(dbContext, userContext), IGoalRepository
{
    public override async Task<Goal> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await Entities
            .AsNoTracking()
            .Include(x => x.Steps)
            .FirstOrDefaultAsync(x => x.Id == id, token);
    }
}
