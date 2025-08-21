using System;
using Goals.Api.Domain.Enums;
using Goals.Api.Domain.ValueObjects;
using Libraries.Common.Entities;

namespace Goals.Api.Domain.Entities;

public class GoalStep : BaseEntity
{
    public Guid GoalId { get; private set; }
    public EntityName Name { get; private set; }
    public string Description { get; private set; }
    public int Order { get; private set; }
    public Progress Progress { get; private set; }
    public GoalStepStatus Status { get; private set; }
    public DateTimeOffset? DueDate { get; private set; }

    private GoalStep() { }

    private GoalStep(EntityName name, string description, int order, DateTimeOffset? dueDate)
    {
        Name = name;
        Description = description;
        Order = order;
        DueDate = dueDate;
        Status = GoalStepStatus.NotStarted;
        Progress = Progress.Zero;
    }

    private GoalStep(Guid goalId, EntityName name, string description, int order, DateTimeOffset? dueDate)
    {
        GoalId = goalId;
        Name = name;
        Description = description;
        Order = order;
        DueDate = dueDate;
        Status = GoalStepStatus.NotStarted;
        Progress = Progress.Zero;
    }

    public static GoalStep Create(Guid goalId, EntityName name, string description, int order, DateTimeOffset? dueDate)
        => new(goalId, name, description, order, dueDate);

    public static GoalStep Create(EntityName name, string description, int order, DateTimeOffset? dueDate)
        => new(name, description, order, dueDate);

    public void Update(Guid goalId, EntityName name, string description, int order, DateTimeOffset? dueDate, Progress progress, GoalStepStatus status)
    {
        GoalId = goalId;
        Name = name;
        Description = description;
        Order = order;
        DueDate = dueDate;
        Progress = Progress.Of(progress.Value);
        SetStatus(status);
    }

    // TODO In case the status changes on one Goal Step 
    // send event. 
    public void SetStatus(GoalStepStatus status)
    {
        Status = status;
        if (Status == GoalStepStatus.Completed)
            Progress = Progress.Of(100);
    }
}
