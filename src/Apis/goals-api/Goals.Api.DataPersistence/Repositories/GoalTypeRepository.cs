using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.DataPersistence.Context;
using Goals.Api.Domain.Entities;
using Libraries.Common.Abstractions;
using Libraries.DataInfrastructure.Repositories;

namespace Goals.Api.DataPersistence.Repositories;

internal sealed class GoalTypeRepository(GoalsDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<GoalType, GoalsDbContext>(dbContext, userContext), IGoalTypeRepository
{ }
