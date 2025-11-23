using System;
using Libraries.Common.Entities;
using Profile.Api.Domain.Abstractions;

namespace Profile.Api.Domain.Models.RatedItems;

public class ProfessionalSkill : BaseEntity, IRatedItem
{
    public int? Rating { get; set; }
    public string Name { get; set; }
    public int YearsOfExperience { get; set; }
    public string Category { get; set; }
    public Guid ProfileId { get; set; }
}