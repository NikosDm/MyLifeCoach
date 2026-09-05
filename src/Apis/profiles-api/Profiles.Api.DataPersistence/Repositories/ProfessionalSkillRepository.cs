using Libraries.DataInfrastructure.Repositories;

using Profiles.Api.Core.Abstractions.Repositories;
using Profiles.Api.DataPersistence.Context;
using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.DataPersistence.Repositories;

internal sealed class ProfessionalSkillRepository(ProfileDbContext dbContext)
    : BaseEntityRepository<ProfessionalSkill, ProfileDbContext>(dbContext),
    IProfessionalSkillRepository
{ }