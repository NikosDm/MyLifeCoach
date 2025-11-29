using System.ComponentModel.DataAnnotations.Schema;

using Profiles.Api.Domain.Abstractions;
using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models.Payloads;

namespace Profiles.Api.Domain.Models;

public sealed class FinancialProfile : ProfileBase
{
    [Column(TypeName = "jsonb")]
    public FinancialProfilePayload Payload { get; set; }

    public FinancialProfile()
    {
        Type = ProfileType.FINANCIAL;
    }
}
