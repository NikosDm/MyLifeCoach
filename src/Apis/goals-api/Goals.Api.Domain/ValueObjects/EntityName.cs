using System;

namespace Goals.Api.Domain.ValueObjects;

public class EntityName
{
    private const int MaxLength = 50;
    public string Value { get; }

    private EntityName(string value) => Value = value;

    public static EntityName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, MaxLength);
        return new EntityName(value);
    }
}
