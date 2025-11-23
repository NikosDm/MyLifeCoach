using Profile.Api.Domain.Enums;

namespace Profile.Api.Domain.Models.Payloads;

public class FitnessProfilePayload
{
    public WeightType WeightType { get; set; }
    public double Weight { get; set; }
    public HeightType HeightType { get; set; }
    public double Height { get; set; }
    public int WorkoutDays { get; set; }
    public string Sport { get; set; }
}
