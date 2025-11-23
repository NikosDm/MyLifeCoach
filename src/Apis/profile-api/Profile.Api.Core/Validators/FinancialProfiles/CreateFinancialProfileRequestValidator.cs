using Profile.Api.Core.Dtos.FinancialProfiles.Requests;

namespace Profile.Api.Core.Validators.FinancialProfiles;

public sealed class CreateFinancialProfileRequestValidator
    : BaseFinancialProfileRequestValidator<CreateFinancialProfileRequest>
{
    public CreateFinancialProfileRequestValidator() : base()
    { }
}