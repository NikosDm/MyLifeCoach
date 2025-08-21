using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.DataPersistence.Context;
using Goals.Api.Domain.Entities;
using Libraries.Common.Abstractions;
using Libraries.DataInfrastructure.Repositories;

namespace Goals.Api.DataPersistence.Repositories;

internal sealed class GoalStepRepository(GoalsDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<GoalStep, GoalsDbContext>(dbContext, userContext), IGoalStepRepository
{ }
