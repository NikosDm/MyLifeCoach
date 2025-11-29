using System;
using System.Threading;
using System.Threading.Tasks;
using Libraries.Common.Abstractions;
using Libraries.DataInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Profiles.Api.Core.Abstractions;
using Profiles.Api.DataPersistence.Context;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.DataPersistence.Repositories;

internal sealed class FitnessProfileRepository(ProfileDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<FitnessProfile, ProfileDbContext>(dbContext, userContext),
    IProfileRepository<FitnessProfile>
{
    public ProfileType Handles => ProfileType.FITNESS;

    public async Task<FitnessProfile> GetByUserIdAsync(Guid userId, CancellationToken token = default)
    {
        return await Entities.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId, token);
    }
}
