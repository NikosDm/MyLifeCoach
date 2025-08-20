using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.DataPersistence.Context;
using Goals.Api.Domain.Entities;
using Libraries.Common.Abstractions;
using Libraries.DataInfrastructure.Repositories;

namespace Goals.Api.DataPersistence.Repositories;

internal sealed class GoalStepRepository(GoalsDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<GoalStep, GoalsDbContext>(dbContext, userContext), IGoalStepRepository
{
    public override GoalStep UpdateEntityValues(GoalStep currentEntity, GoalStep modifiedEntity)
    {
        currentEntity.Name = modifiedEntity.Name;
        currentEntity.Description = modifiedEntity.Description;
        currentEntity.Order = modifiedEntity.Order;
        currentEntity.DueDate = modifiedEntity.DueDate;
        currentEntity.IsActive = modifiedEntity.IsActive;
        currentEntity.Progress = modifiedEntity.Progress;
        return base.UpdateEntityValues(currentEntity, modifiedEntity);
    }
}
