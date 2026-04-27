using System;
using System.Threading;
using System.Threading.Tasks;

using Libraries.Common.Abstractions;
using Libraries.Common.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Libraries.DataInfrastructure.Interceptors;

public class AuditableEntityInterceptor(IUserContext userContext) : SaveChangesInterceptor
{
    private readonly TimeProvider _timeProvider = TimeProvider.System;
    private readonly IUserContext _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetEntityId(Guid.NewGuid());
                    entry.Entity.CreatedAt = _timeProvider.GetUtcNow();
                    entry.Entity.LastUpdatedAt = _timeProvider.GetUtcNow();
                    entry.Entity.CreatedBy = _userContext.UserId ?? Guid.Empty;
                    entry.Entity.LastUpdatedBy = _userContext.UserId ?? Guid.Empty;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastUpdatedAt = _timeProvider.GetUtcNow();
                    entry.Entity.LastUpdatedBy = _userContext.UserId ?? Guid.Empty;
                    break;
                default:
                    break;
            }
        }
    }
}