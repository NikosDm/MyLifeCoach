namespace Goals.Api.Domain.Enums;

/// <summary>Represents the lifecycle status of a goal.</summary>
public enum GoalStatus
{
    /// <summary>Goal not started yet.</summary>
    NotStarted,
    /// <summary>Goal is currently active.</summary>
    Active,
    /// <summary>Goal has been completed successfully.</summary>
    Completed,
    /// <summary>Goal is temporarily paused.</summary>
    Paused,
    /// <summary>Goal is cancelled (deactivated).</summary>
    Cancelled
}