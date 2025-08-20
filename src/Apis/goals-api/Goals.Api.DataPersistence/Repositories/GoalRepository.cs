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
    public override async Task<IReadOnlyList<Goal>> GetAsync(CancellationToken token = default)
    {
        return await Entities
            .AsNoTracking()
            .Include(x => x.Type)
            .ToListAsync(token);
    }

    public override async Task<Goal> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await Entities
            .AsNoTracking()
            .Include(x => x.Type)
            .Include(x => x.Steps)
            .FirstOrDefaultAsync(token);
    }

    public override Goal UpdateEntityValues(Goal currentEntity, Goal modifiedEntity)
    {
        currentEntity.Name = modifiedEntity.Name;
        currentEntity.Description = modifiedEntity.Description;
        currentEntity.StartDate = modifiedEntity.StartDate;
        currentEntity.EndDate = modifiedEntity.EndDate;
        currentEntity.TypeId = modifiedEntity.TypeId;
        currentEntity.Status = modifiedEntity.Status;
        currentEntity.Progress = modifiedEntity.Progress;
        return base.UpdateEntityValues(currentEntity, modifiedEntity);
    }
}
