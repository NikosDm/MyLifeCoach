using System;
using System.Collections.Generic;
using Goals.Api.Domain.Enums;
using Libraries.Common.Entities;

namespace Goals.Api.Domain.Entities;

public class Goal : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid TypeId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public GoalStatus Status { get; set; }
    public double Progress { get; set; }

    public GoalType Type { get; set; }
    
    public ICollection<GoalStep> Steps { get; set; }
}