using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.Core.Abstractions.Repositories;

public interface IProfessionalSkillRepository
{
    Task<IReadOnlyList<ProfessionalSkill>> GetAsync(CancellationToken token = default);
    Task<ProfessionalSkill> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IReadOnlyList<ProfessionalSkill>> SearchAsync(Expression<Func<ProfessionalSkill, bool>> options = null, CancellationToken token = default);
    Task<ProfessionalSkill> CreateAsync(ProfessionalSkill entity, bool saveChanges = true, CancellationToken token = default);
    Task<ProfessionalSkill> UpdateAsync(ProfessionalSkill entity, bool saveChanges = true, CancellationToken token = default);
}