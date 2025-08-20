using System;
using System.Collections.Generic;
using Goals.Api.Core.Dtos.GoalSteps.Requests;

namespace Goals.Api.Core.Dtos.Goals.Requests;

/// <summary>
/// Request used for creating a goal 
/// </summary>
/// <param name="Name">Name of goal (Required - should not exceed 50 characters)</param>
/// <param name="Description">Description of goal (optional)</param>
/// <param name="TypeId">Type Id (Required - Guid value)</param>
/// <param name="StartDate">Start date of goal (Required)</param>
/// <param name="EndDate">End date of goal (optional)</param>
/// <param name="Steps">List of goal steps (check <see cref="CreateGoalStepRequest"/> for full request schema)</param>
public record CreateGoalRequest(string Name, string Description, Guid TypeId,
    DateTimeOffset StartDate, DateTimeOffset? EndDate, IEnumerable<CreateGoalStepRequest> Steps); 
