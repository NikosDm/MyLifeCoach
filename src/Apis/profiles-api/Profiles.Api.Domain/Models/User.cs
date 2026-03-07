using Libraries.Common.Entities;

namespace Profiles.Api.Domain.Models;

public sealed class User : BaseEntity
{
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public PersonalProfile PersonalProfile { get; set; }
    public ProfessionalProfile ProfessionalProfile { get; set; }
    public FinancialProfile FinancialProfile { get; set; }
    public FitnessProfile FitnessProfile { get; set; }
}