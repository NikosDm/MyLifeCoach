using System;
using Libraries.Common.Abstractions;
using Libraries.Common.Constants;
using Libraries.Common.Exceptions;

namespace Goals.Api.Domain.ValueObjects;

public class GoalPeriod : IPeriod
{
    public DateTimeOffset StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }

    private GoalPeriod(DateTimeOffset startDate, DateTimeOffset? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static GoalPeriod Of(DateTimeOffset startDate, DateTimeOffset? endDate)
    {
        if (endDate.HasValue && startDate > endDate)
            throw new DomainException(ValidationErrorLiterals.InvalidGivenPeriod);

        return new GoalPeriod(startDate, endDate);
    }
}
