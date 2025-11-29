namespace Profiles.Api.Core.Dtos.FinancialProfiles.Requests;

public sealed record UpdateFinancialProfileRequest(double AnnualNetIncome, string Currency)
    : BaseFinancialProfileRequest(AnnualNetIncome, Currency);