using System;
using Libraries.Common.Exceptions;

namespace Libraries.Common.Entities;

public abstract class BaseEntity : IEntity<Guid>
{
    public Guid Id { get; private set; }    
    public Guid CreatedBy { get; set; }
    public Guid LastUpdatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastUpdatedAt { get; set; }

    public void SetEntityId(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("Entity Id value cannot be empty");
        }

        Id = value;
    }
}