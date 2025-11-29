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

internal sealed class ProfessionalProfileRepository(ProfileDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<ProfessionalProfile, ProfileDbContext>(dbContext, userContext),
    IProfileRepository<ProfessionalProfile>
{
    public ProfileType Handles => ProfileType.PROFESSIONAL;

    public async Task<ProfessionalProfile> GetByUserIdAsync(Guid userId, CancellationToken token = default)
    {
        return await Entities.AsNoTracking()
            .Include(x => x.ProfessionalSkills)
            .FirstOrDefaultAsync(x => x.UserId == userId, token);
    }
}
