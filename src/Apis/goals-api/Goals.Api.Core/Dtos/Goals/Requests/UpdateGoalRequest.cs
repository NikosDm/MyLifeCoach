using System;
using Goals.Api.Domain.Enums;

namespace Goals.Api.Core.Dtos.Goals.Requests;

/// <summary>
/// Request used for updating a goal properties. 
/// </summary>
/// <param name="Name">Name of goal (Required - should not exceed 50 characters)</param>
/// <param name="Description">Description of goal (optional)</param>
/// <param name="TypeId">Type Id (Required - Guid value)</param>
/// <param name="StartDate">Start date of goal (Required)</param>
/// <param name="EndDate">End date of goal (optional)</param>
/// <param name="Status">Goal status (optional - check <see cref="GoalStatus"/> for values)</param>
/// <param name="Progress">Progress percentage (double - should be between 0 and 100)</param>
public record UpdateGoalRequest(string Name, string Description, Guid TypeId,
    DateTimeOffset StartDate, DateTimeOffset? EndDate, GoalStatus Status, double Progress);
