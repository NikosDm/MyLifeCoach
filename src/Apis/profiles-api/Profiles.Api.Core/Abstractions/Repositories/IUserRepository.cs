using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Models;

using Profiles.Api.Domain.Models;

namespace Profiles.Api.Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task<PaginationResult<UserListItemResponse>> GetPaginatedAsync<UserListItemResponse>(
        int pageNumber,
        int pageSize,
        Expression<Func<User, UserListItemResponse>> selectOptions,
        Expression<Func<User, bool>> whereOptions = null,
        Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
        CancellationToken token = default);
    Task<IReadOnlyList<User>> GetAsync(CancellationToken token = default);
    Task<User> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<User> CreateAsync(User user, bool saveChanges = true, CancellationToken token = default);
}