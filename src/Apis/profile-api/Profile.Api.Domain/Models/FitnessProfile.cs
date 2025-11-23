using System.ComponentModel.DataAnnotations.Schema;

using Profile.Api.Domain.Abstractions;
using Profile.Api.Domain.Enums;
using Profile.Api.Domain.Models.Payloads;

namespace Profile.Api.Domain.Models;

public sealed class FitnessProfile : ProfileBase
{
    [Column(TypeName = "jsonb")]
    public FitnessProfilePayload Payload { get; set; }

    public FitnessProfile()
    {
        Type = ProfileType.FITNESS;
    }
}
