using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Profiles.Api.Domain.Abstractions;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models.Payloads;
using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.Domain.Models;

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
