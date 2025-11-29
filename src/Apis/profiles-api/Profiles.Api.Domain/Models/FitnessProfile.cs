using System.ComponentModel.DataAnnotations.Schema;

using Profiles.Api.Domain.Abstractions;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models.Payloads;

namespace Profiles.Api.Domain.Models;

public sealed class FitnessProfile : ProfileBase
{
    [Column(TypeName = "jsonb")]
    public FitnessProfilePayload Payload { get; set; }

    public FitnessProfile()
    {
        Type = ProfileType.FITNESS;
    }
}
