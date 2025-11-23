using System.ComponentModel.DataAnnotations.Schema;

using Profile.Api.Domain.Abstractions;
using Profile.Api.Domain.Enums;
using Profile.Api.Domain.Models.Payloads;

namespace Profile.Api.Domain.Models;

public sealed class FinancialProfile : ProfileBase
{
    [Column(TypeName = "jsonb")]
    public FinancialProfilePayload Payload { get; set; }

    public FinancialProfile()
    {
        Type = ProfileType.FINANCIAL;
    }
}
