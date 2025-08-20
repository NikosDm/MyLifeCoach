namespace Goals.Api.Core.Dtos.GoalTypes.Requests;

/// <summary>
/// Request for creating a Goal Type
/// </summary>
/// <param name="Name">Name of Goal type (Required - should not exceed 50 characters)</param>
/// <param name="Description">Description of Goal type (Optional)</param>
public record CreateGoalTypeRequest(string Name, string Description);
