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

internal sealed class FinancialProfileRepository(ProfileDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<FinancialProfile, ProfileDbContext>(dbContext, userContext),
    IProfileRepository<FinancialProfile>
{
    public ProfileType Handles => ProfileType.FINANCIAL;

    public async Task<FinancialProfile> GetByUserIdAsync(Guid userId, CancellationToken token = default)
    {
        return await Entities.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId, token);
    }
}
