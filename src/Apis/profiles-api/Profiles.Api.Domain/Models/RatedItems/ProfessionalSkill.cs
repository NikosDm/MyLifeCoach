using System;
using Libraries.Common.Entities;
using Profiles.Api.Domain.Abstractions;

namespace Profiles.Api.Domain.Models.RatedItems;

public class ProfessionalSkill : BaseEntity, IRatedItem
{
    public int? Rating { get; set; }
    public string Name { get; set; }
    public int YearsOfExperience { get; set; }
    public string Category { get; set; }
    public Guid ProfileId { get; set; }
}