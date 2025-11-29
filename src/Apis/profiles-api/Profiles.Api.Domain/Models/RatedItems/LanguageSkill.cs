using System;

using Libraries.Common.Entities;

using Profiles.Api.Domain.Abstractions;

namespace Profiles.Api.Domain.Models.RatedItems;

public class LanguageSkill : BaseEntity, IRatedItem
{
    public Guid ProfileId { get; set; }
    public int? Rating { get; set; }
    public string LanguageCode { get; set; }
    public bool? IsNative { get; set; }
}