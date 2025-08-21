using System;

namespace Goals.Api.Core.Dtos.GoalSteps.Requests;

/// <summary>
/// Goal step request. This is used when a Goal is created along with steps on endpoint
/// </summary>
/// <param name="Name">Name of step (Required - should not exceed 50 characters)</param>
/// <param name="Description">Description of step (Optional)</param>
/// <param name="Order">Order of the step (Integer - should not be negative)</param>
/// <param name="DueDate">Due date of step (Optional)</param>
public record CreateGoalStepRequest(string Name, string Description, int Order, DateTimeOffset? DueDate);

/// <summary>
/// Goal step request. This is used when a Goal is created along with steps on endpoint
/// </summary>
/// <param name="GoalId">Goal ID (Required - Guid)</param>
/// <param name="Name">Name of step (Required - should not exceed 50 characters)</param>
/// <param name="Description">Description of step (Optional)</param>
/// <param name="Order">Order of the step (Integer - should not be negative)</param>
/// <param name="DueDate">Due date of step (Optional)</param>
public sealed record CreateStepForGoalRequest(Guid GoalId, string Name, string Description, int Order, DateTimeOffset? DueDate) : CreateGoalStepRequest(Name, Description, Order, DueDate);