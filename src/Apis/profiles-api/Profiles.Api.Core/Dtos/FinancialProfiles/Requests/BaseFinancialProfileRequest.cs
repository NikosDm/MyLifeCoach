namespace Profiles.Api.Core.Dtos.FinancialProfiles.Requests;

public abstract record BaseFinancialProfileRequest(double AnnualNetIncome, string Currency);