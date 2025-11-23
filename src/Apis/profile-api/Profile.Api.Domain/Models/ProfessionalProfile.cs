using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Profile.Api.Domain.Abstractions;
using Profile.Api.Domain.Models.Payloads;
using Profile.Api.Domain.Models.RatedItems;

namespace Profile.Api.Domain.Models;

public sealed class ProfessionalProfile : ProfileBase
{
    [Column(TypeName = "jsonb")]
    public ProfessionalProfilePayload Payload { get; set; }
    public ICollection<ProfessionalSkill> ProfessionalSkills { get; set; }

    public ProfessionalProfile()
    {
        Type = Enums.ProfileType.PROFESSIONAL;
    }
}
