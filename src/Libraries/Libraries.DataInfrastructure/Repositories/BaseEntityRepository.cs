using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Libraries.DataInfrastructure.Enums;
using Libraries.Common.Entities;
using Libraries.Common.Abstractions;

namespace Libraries.DataInfrastructure.Repositories;

public abstract class BaseEntityRepository<TEntity, TContext>(TContext dbContext, IUserContext userContext)
    where TEntity : BaseEntity
    where TContext : DbContext
{
    protected readonly DbSet<TEntity> Entities = dbContext.Set<TEntity>();
    private readonly TContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly TimeProvider _timeProvider = TimeProvider.System;
    private readonly IUserContext _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken token = default)
    {
        SetAuditValues(entity, AuditAction.Create);
        var addedEntity = await Entities.AddAsync(entity, token);
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

    public virtual async Task<IReadOnlyList<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> options = null, CancellationToken token = default)
    {
        var query = Entities.AsNoTracking();

        if (options is null)
            return await query.ToListAsync(token);

        return await query.Where(options).ToListAsync(token);
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        SetAuditValues(entity, AuditAction.Udpate);
        await StoreChangesAsync(token);

        return entity;
    }

    private void SetAuditValues(TEntity entity, AuditAction auditAction)
    {
        switch (auditAction)
        {
            case AuditAction.Create:
                entity.SetEntityId(Guid.NewGuid());
                entity.CreatedAt = _timeProvider.GetUtcNow();
                entity.LastUpdatedAt = _timeProvider.GetUtcNow();
                entity.CreatedBy = _userContext.UserId ?? Guid.Empty;
                entity.LastUpdatedBy = _userContext.UserId ?? Guid.Empty;
                break;
            case AuditAction.Udpate:
                entity.LastUpdatedAt = _timeProvider.GetUtcNow();
                entity.LastUpdatedBy = _userContext.UserId ?? Guid.Empty;
                break;
            default:
                break;
        }
    }

    private async Task<bool> StoreChangesAsync(CancellationToken token = default)
        => await _dbContext.SaveChangesAsync(token) > 0;
}
