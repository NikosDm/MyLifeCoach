using System;
using Libraries.Common.Exceptions;

namespace Goals.Api.Domain.ValueObjects;

public class Progress
{
    public double Value { get; }

    private Progress(double value) => Value = value;

    public static Progress Of(double value)
    {
        if (value < 0 || value > 100) throw new DomainException("Percentage value is invalid, it should be 0 and 100.");
        return new Progress(Math.Round(value, 2));
    }

    public static Progress Zero => new(0);
}
