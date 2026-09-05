using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Libraries.Common.Entities;
using Libraries.Common.Models;

namespace Libraries.DataInfrastructure.Repositories;

public abstract class BaseEntityRepository<TEntity, TContext>(TContext dbContext)
    where TEntity : BaseEntity
    where TContext : DbContext
{
    protected readonly DbSet<TEntity> Entities = dbContext.Set<TEntity>();
    private readonly TContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    /* START - PAGINATION METHODS*/

    public virtual async Task<PaginationResult<TResult>> GetPaginatedAsync<TResult>(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, TResult>> selectOptions,
        Expression<Func<TEntity, bool>> whereOptions = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(selectOptions, nameof(selectOptions));

        var query = Entities.AsNoTracking();

        if (whereOptions is not null)
            query = query.Where(whereOptions);

        var totalCount = await query.CountAsync(token);

        var orderedQuery = orderBy is not null
            ? orderBy(query)
            : query.OrderByDescending(x => x.LastUpdatedAt);

        var items = await orderedQuery
            .Select(selectOptions)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        return new PaginationResult<TResult>(items, totalCount, pageNumber, pageSize);
    }

    /* END - PAGINATION METHODS*/

    public virtual async Task<TEntity> CreateAsync(TEntity entity, bool saveChanges = true, CancellationToken token = default)
    {
        var addedEntity = await Entities.AddAsync(entity, token);
        if (saveChanges)
            await StoreChangesAsync(token);

        return addedEntity.Entity;
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAsync(CancellationToken token = default)
    {
        return await Entities.AsNoTracking().ToListAsync(token);
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await Entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public virtual async Task<IReadOnlyList<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> whereOptions = null, CancellationToken token = default)
    {
        var query = Entities.AsNoTracking();

        return whereOptions is null
            ? await query.ToListAsync(token)
            : await query.Where(whereOptions).ToListAsync(token);
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true, CancellationToken token = default)
    {
        Entities.Update(entity);
        if (saveChanges)
            await StoreChangesAsync(token);

        return entity;
    }

    public virtual async Task DeleteAsync(TEntity entity, bool saveChanges = true, CancellationToken token = default)
    {
        Entities.Remove(entity);
        if (saveChanges)
            await StoreChangesAsync(token);
    }

    public async Task<bool> StoreChangesAsync(CancellationToken token = default)
        => await _dbContext.SaveChangesAsync(token) > 0;
}
