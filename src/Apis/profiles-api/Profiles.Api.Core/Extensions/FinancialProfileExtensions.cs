using Profiles.Api.Core.Dtos.FinancialProfiles.Requests;
using Profiles.Api.Core.Dtos.FinancialProfiles.Responses;
using Profiles.Api.Domain.Models;
using Profiles.Api.Domain.Models.Payloads;

namespace Profiles.Api.Core.Extensions;

public static class FinancialProfileExtensions
{
    public static FinancialProfileResponse ToResponse(this FinancialProfile source)
        => source is null ? null
        : new FinancialProfileResponse(
            source.Id,
            source.UserId,
            source.Payload.AnnualNetIncome,
            source.Payload.Currency);

    public static FinancialProfile ToEntity(this CreateFinancialProfileRequest source)
        => source is null ? null
        : new()
        {
            Payload = new FinancialProfilePayload
            {
                AnnualNetIncome = source.AnnualNetIncome,
                Currency = source.Currency
            }
        };

    public static FinancialProfile MapRequestToEntity(this UpdateFinancialProfileRequest source, FinancialProfile target)
    {
        if (source is null || target is null) return target;

        target.Payload.AnnualNetIncome = source.AnnualNetIncome;
        target.Payload.Currency = source.Currency;
        return target;
    }
}
