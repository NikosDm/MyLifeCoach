using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.DataPersistence.Context;
using Goals.Api.Domain.Entities;

using Libraries.DataInfrastructure.Repositories;

namespace Goals.Api.DataPersistence.Repositories;

internal sealed class GoalStepRepository(GoalsDbContext dbContext)
    : BaseEntityRepository<GoalStep, GoalsDbContext>(dbContext), IGoalStepRepository
{ }
