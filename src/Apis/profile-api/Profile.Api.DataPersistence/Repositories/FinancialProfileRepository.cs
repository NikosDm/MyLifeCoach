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
