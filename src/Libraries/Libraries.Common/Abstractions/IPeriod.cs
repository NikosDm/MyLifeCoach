using System;

namespace Libraries.Common.Abstractions;

public interface IPeriod
{
    DateTimeOffset StartDate { get; }
    DateTimeOffset? EndDate { get; }
}
