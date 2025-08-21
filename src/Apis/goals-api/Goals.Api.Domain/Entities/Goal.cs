using System;
using System.Collections.Generic;
using System.Linq;
using Goals.Api.Domain.Enums;
using Goals.Api.Domain.ValueObjects;
using Libraries.Common.Entities;

namespace Goals.Api.Domain.Entities;

public class Goal : BaseEntity
{
    private readonly List<GoalStep> _steps = [];
    public IReadOnlyList<GoalStep> Steps => _steps.AsReadOnly();

    public EntityName Name { get; private set; }
    public string Description { get; private set; }
    public Guid TypeId { get; private set; }
    public GoalPeriod Period { get; private set; }
    public GoalStatus Status { get; private set; }
    public Progress Progress { get; private set; }

    private Goal() { }

    private Goal(EntityName name, string description, Guid goalTypeId, GoalPeriod period)
    {
        Name = name;
        Description = description;
        TypeId = goalTypeId;
        Period = period;
        Status = GoalStatus.NotStarted;
        Progress = Progress.Zero;
    }

    public static Goal Create(EntityName name, string description, Guid goalTypeId, GoalPeriod period)
        => new(name, description, goalTypeId, period);

    public void Update(EntityName name, string description, Guid goalTypeId, GoalPeriod period, GoalStatus status)
    {
        CheckStatus(status);
        Name = name;
        Description = description;
        TypeId = goalTypeId;
        Period = period;
        RecalculateProgress();
    }

    private void CheckStatus(GoalStatus newStatus)
    {
        if (Status == newStatus) return;

        switch (newStatus)
        {
            case GoalStatus.Active:
                if (Status is GoalStatus.Completed or GoalStatus.Cancelled)
                    throw new InvalidOperationException("Cannot activate completed or cancelled goal.");
                Status = GoalStatus.Active;
                foreach (var s in _steps.Where(s => s.Status is GoalStepStatus.Inactive or GoalStepStatus.NotStarted))
                    s.SetStatus(GoalStepStatus.InProgress);
                break;

            case GoalStatus.Paused:
                if (Status != GoalStatus.Active)
                    throw new InvalidOperationException("Only active goals can be paused.");
                Status = GoalStatus.Paused;
                foreach (var s in _steps.Where(s => s.Status is GoalStepStatus.InProgress))
                    s.SetStatus(GoalStepStatus.Inactive);
                break;

            case GoalStatus.Cancelled:
                Status = GoalStatus.Cancelled;
                foreach (var s in _steps.Where(s => s.Status is not GoalStepStatus.Completed))
                    s.SetStatus(GoalStepStatus.Deleted);
                break;

            case GoalStatus.Completed:
                if (_steps.Any(s => s.Status != GoalStepStatus.Completed))
                    throw new InvalidOperationException("All steps must be completed first.");
                Status = GoalStatus.Completed;
                Progress = Progress.Of(100);
                break;

            case GoalStatus.NotStarted:
                if (_steps.Any(s => s.Status is GoalStepStatus.InProgress or GoalStepStatus.Completed))
                    throw new InvalidOperationException("Cannot revert to NotStarted when steps advanced.");
                Status = GoalStatus.NotStarted;
                break;
        }
    }

    private void RecalculateProgress()
    {
        if (_steps.Count == 0) return;
        var avg = _steps.Average(s => s.Progress.Value);
        Progress = Progress.Of(avg);
    }

    public void AddGoalStep(GoalStep step)
    {
        _steps.Add(step);
        RecalculateProgress();
    }
}