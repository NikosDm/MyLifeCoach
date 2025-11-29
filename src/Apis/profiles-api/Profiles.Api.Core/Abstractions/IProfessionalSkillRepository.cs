using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.Core.Abstractions;

public interface IProfessionalSkillRepository
{
    Task<IReadOnlyList<ProfessionalSkill>> GetAsync(CancellationToken token = default);
    Task<ProfessionalSkill> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IReadOnlyList<ProfessionalSkill>> SearchAsync(Expression<Func<ProfessionalSkill, bool>> options = null, CancellationToken token = default);
    Task<ProfessionalSkill> CreateAsync(ProfessionalSkill entity, CancellationToken token = default);
    Task<ProfessionalSkill> UpdateAsync(ProfessionalSkill entity, CancellationToken token = default);
}