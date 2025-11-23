using System;

namespace Profile.Api.Core.Dtos.FinancialProfiles.Responses;

public sealed record FinancialProfileResponse(
    Guid ProfileId,
    Guid UserId,
    double AnnualNetIncome,
    string Currency);
