using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.DataPersistence.Context;
using Goals.Api.Domain.Entities;

using Libraries.DataInfrastructure.Repositories;

namespace Goals.Api.DataPersistence.Repositories;

internal sealed class GoalTypeRepository(GoalsDbContext dbContext)
    : BaseEntityRepository<GoalType, GoalsDbContext>(dbContext), IGoalTypeRepository
{ }
