using Libraries.Common.Abstractions;
using Libraries.DataInfrastructure.Repositories;

using Profile.Api.Core.Abstractions;
using Profile.Api.DataPersistence.Context;
using Profile.Api.Domain.Models.RatedItems;

namespace Profile.Api.DataPersistence.Repositories;

internal sealed class ProfessionalSkillRepository(ProfileDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<ProfessionalSkill, ProfileDbContext>(dbContext, userContext),
    IProfessionalSkillRepository
{ }