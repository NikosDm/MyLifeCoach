using System;

namespace Goals.Api.Core.Dtos.GoalSteps.Requests;

/// <summary>
/// Request used for updating a goal step
/// </summary>
/// <param name="GoalId">Goal ID (Required - Guid)</param>
/// <param name="GoalId">Goal ID (Required - Guid)</param>
/// <param name="Name">Name of step (Required - should not exceed 50 characters)</param>
/// <param name="Description">Description of step (Optional)</param>
/// <param name="Order">Order of the step (Integer - should not be negative)</param>
/// <param name="DueDate">Due date of step (Optional)</param>
/// <param name="IsActive">Determining whether step is active or not (true or false)</param>
public record UpdateGoalStepRequest(Guid GoalId, string Name, string Description, int Order, double Progress, DateTimeOffset? DueDate, bool IsActive);
