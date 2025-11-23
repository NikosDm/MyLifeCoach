using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Profile.Api.Domain.Abstractions;
using Profile.Api.Domain.Enums;
using Profile.Api.Domain.Models.Payloads;
using Profile.Api.Domain.Models.RatedItems;

namespace Profile.Api.Domain.Models;

public sealed class PersonalProfile : ProfileBase
{
    [Column(TypeName = "jsonb")]
    public PersonalProfilePayload Payload { get; set; }
    public ICollection<LanguageSkill> LanguageSkills { get; set; }

    public PersonalProfile()
    {
        Type = ProfileType.PERSONAL;
    }
}
