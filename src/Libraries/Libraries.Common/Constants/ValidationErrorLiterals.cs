using System;

namespace Libraries.Common.Constants;

public static class ValidationErrorLiterals
{
    public const string NotEmptyParameter = "{0} should not be empty";
    public const string ParameterExceedLimit = "{0} should not exceed {1} characters";
    public const string NegativeNumericParameterValue = "{0} cannot have a negative value";
    public const string InvalidGivenPeriod = "EndDate must be later than StartDate.";
}
