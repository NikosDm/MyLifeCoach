using Libraries.DataInfrastructure.Repositories;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.DataPersistence.Context;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.DataPersistence.Repositories;

internal sealed class UserRepository(ProfileDbContext dbContext)
    : BaseEntityRepository<User, ProfileDbContext>(dbContext),
    IUserRepository
{ }