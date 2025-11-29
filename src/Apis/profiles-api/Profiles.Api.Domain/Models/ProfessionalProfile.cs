using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Profiles.Api.Domain.Abstractions;
using Profiles.Api.Domain.Models.Payloads;
using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.Domain.Models;

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
