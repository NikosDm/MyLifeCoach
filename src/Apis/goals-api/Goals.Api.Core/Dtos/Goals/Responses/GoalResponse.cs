using System;
using System.Collections.Generic;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Goals.Api.Domain.Enums;

namespace Goals.Api.Core.Dtos.Goals.Responses;

public sealed record GoalResponse
{
    public Guid Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public Guid TypeId { get; init; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }
    public GoalStatus Status { get; init; }
    public double Progress { get; init; }
    
    public IEnumerable<GoalStepResponse> Steps { get; init; }  
}