using System;
using Goals.Api.Domain.Enums;

namespace Goals.Api.Core.Dtos.GoalSteps.Responses;

public sealed record GoalStepResponse
{
    public Guid Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Order { get; init; }
    public double Progress { get; init; }
    public GoalStepStatus Status { get; set; }
    public DateTimeOffset? DueDate { get; init; }
}