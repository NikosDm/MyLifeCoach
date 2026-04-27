using System;
using System.Threading;
using System.Threading.Tasks;

using Libraries.DataInfrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.DataPersistence.Context;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.DataPersistence.Repositories;

internal sealed class PersonalProfileRepository(ProfileDbContext dbContext)
    : BaseEntityRepository<PersonalProfile, ProfileDbContext>(dbContext),
    IProfileRepository<PersonalProfile>
{
    public ProfileType Handles => ProfileType.PERSONAL;

    public async Task<PersonalProfile> GetByUserIdAsync(Guid userId, CancellationToken token = default)
    {
        return await Entities
            .Include(u => u.User)
            .AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId, token);
    }
}
