using System;

namespace Libraries.Common.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }    
    public Guid CreatedBy { get; set; }
    public Guid LastUpdatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastUpdatedAt { get; set; }
}