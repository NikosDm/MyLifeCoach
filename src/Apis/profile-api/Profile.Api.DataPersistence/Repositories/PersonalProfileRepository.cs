using System;
using System.Threading;
using System.Threading.Tasks;
using Libraries.Common.Abstractions;
using Libraries.DataInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Profile.Api.Core.Abstractions;
using Profile.Api.DataPersistence.Context;
using Profile.Api.Domain.Enums;
using Profile.Api.Domain.Models;

namespace Profile.Api.DataPersistence.Repositories;

internal sealed class PersonalProfileRepository(ProfileDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<PersonalProfile, ProfileDbContext>(dbContext, userContext),
    IProfileRepository<PersonalProfile>
{
    public ProfileType Handles => ProfileType.PERSONAL;

    public async Task<PersonalProfile> GetByUserIdAsync(Guid userId, CancellationToken token = default)
    {
        return await Entities.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId, token);
    }
}
