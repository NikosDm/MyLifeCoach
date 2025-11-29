using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.Core.Abstractions;

public interface ILanguageSkillRepository
{
    Task<IReadOnlyList<LanguageSkill>> GetAsync(CancellationToken token = default);
    Task<LanguageSkill> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IReadOnlyList<LanguageSkill>> SearchAsync(Expression<Func<LanguageSkill, bool>> options = null, CancellationToken token = default);
    Task<LanguageSkill> CreateAsync(LanguageSkill entity, CancellationToken token = default);
    Task<LanguageSkill> UpdateAsync(LanguageSkill entity, CancellationToken token = default);
}