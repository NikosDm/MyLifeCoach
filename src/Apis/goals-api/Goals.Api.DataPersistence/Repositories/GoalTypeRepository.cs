using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.DataPersistence.Context;
using Goals.Api.Domain.Entities;
using Libraries.Common.Abstractions;
using Libraries.DataInfrastructure.Repositories;

namespace Goals.Api.DataPersistence.Repositories;

internal sealed class GoalTypeRepository(GoalsDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<GoalType, GoalsDbContext>(dbContext, userContext), IGoalTypeRepository
{
    public override GoalType UpdateEntityValues(GoalType currentEntity, GoalType modifiedEntity)
    {
        currentEntity.Name = modifiedEntity.Name;
        currentEntity.Description = modifiedEntity.Description;
        currentEntity.IsActive = modifiedEntity.IsActive;
        return base.UpdateEntityValues(currentEntity, modifiedEntity);
    }
}
