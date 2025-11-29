using Libraries.Common.Abstractions;
using Libraries.DataInfrastructure.Repositories;

using Profiles.Api.Core.Abstractions;
using Profiles.Api.DataPersistence.Context;
using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.DataPersistence.Repositories;

internal sealed class LanguageSkillRepository(ProfileDbContext dbContext, IUserContext userContext)
    : BaseEntityRepository<LanguageSkill, ProfileDbContext>(dbContext, userContext),
    ILanguageSkillRepository
{ }