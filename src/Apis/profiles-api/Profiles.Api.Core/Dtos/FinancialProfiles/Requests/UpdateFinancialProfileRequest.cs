using System;

namespace Profiles.Api.Core.Dtos.FinancialProfiles.Requests;

public sealed record UpdateFinancialProfileRequest(Guid UserId, double AnnualNetIncome, string Currency)
    : BaseFinancialProfileRequest(UserId, AnnualNetIncome, Currency);