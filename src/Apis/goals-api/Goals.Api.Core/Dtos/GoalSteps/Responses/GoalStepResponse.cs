using System;

namespace Goals.Api.Core.Dtos.GoalSteps.Responses;

public record GoalStepResponse
{
    public Guid Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Order { get; init; }
    public double Progress { get; init; }
    public bool IsActive { get; init; }
    public DateTimeOffset? DueDate { get; init; }
}