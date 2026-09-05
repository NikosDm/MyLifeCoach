using System;

namespace Profiles.Api.Core.Dtos.FinancialProfiles.Requests;

public abstract record BaseFinancialProfileRequest(Guid UserId, double AnnualNetIncome, string Currency) : BaseProfileRequest(UserId);