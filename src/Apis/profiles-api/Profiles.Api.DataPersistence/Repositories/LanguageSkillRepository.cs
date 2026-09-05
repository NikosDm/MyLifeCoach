using Libraries.DataInfrastructure.Repositories;

using Profiles.Api.Core.Abstractions.Repositories;
using Profiles.Api.DataPersistence.Context;
using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.DataPersistence.Repositories;

internal sealed class LanguageSkillRepository(ProfileDbContext dbContext)
    : BaseEntityRepository<LanguageSkill, ProfileDbContext>(dbContext),
    ILanguageSkillRepository
{ }