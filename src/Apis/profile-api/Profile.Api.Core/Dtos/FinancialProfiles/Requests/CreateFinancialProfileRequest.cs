namespace Profile.Api.Core.Dtos.FinancialProfiles.Requests;

public sealed record CreateFinancialProfileRequest(double AnnualNetIncome, string Currency)
    : BaseFinancialProfileRequest(AnnualNetIncome, Currency);
