using System;
using Libraries.Common.Entities;

namespace Goals.Api.Domain.Entities;

public class GoalStep : BaseEntity
{
    public Guid GoalId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public double Progress { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset? DueDate { get; set; }

    public Goal Goal { get; set; }
}
