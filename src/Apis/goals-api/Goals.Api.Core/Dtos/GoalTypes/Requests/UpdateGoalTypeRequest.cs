namespace Goals.Api.Core.Dtos.GoalTypes.Requests;

/// <summary>
/// Request for updating a Goal Type
/// </summary>
/// <param name="Name">Name of Goal type (Required - should not exceed 50 characters)</param>
/// <param name="Description">Description of Goal type (Optional)</param>
/// <param name="IsActive">Flag determining whether Goal Type is active or not (Optional - true or false)</param>
public sealed record UpdateGoalTypeRequest(string Name, string Description, bool IsActive);
