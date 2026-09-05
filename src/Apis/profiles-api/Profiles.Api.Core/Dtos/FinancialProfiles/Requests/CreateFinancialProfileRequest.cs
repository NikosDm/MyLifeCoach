using System;

namespace Profiles.Api.Core.Dtos.FinancialProfiles.Requests;

public sealed record CreateFinancialProfileRequest(Guid UserId, double AnnualNetIncome, string Currency)
    : BaseFinancialProfileRequest(UserId, AnnualNetIncome, Currency);
