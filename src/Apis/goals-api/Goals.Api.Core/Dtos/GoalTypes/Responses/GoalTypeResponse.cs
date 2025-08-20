using System;

namespace Goals.Api.Core.Dtos.GoalTypes.Responses;

public record GoalTypeResponse
{
    public Guid Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool IsActive { get; init; }
}