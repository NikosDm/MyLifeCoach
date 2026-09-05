using System;

using Libraries.Common.Enums;

namespace Libraries.Common.Models;

public record SortField<TSortField> where TSortField : struct, Enum
{
    public TSortField Field { get; init; }

    public SortDirection Direction { get; init; }

    public static bool TryParse(
        string value,
        IFormatProvider provider,
        out SortField<TSortField> result)
    {
        result = null;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        var parts = value.Split(
            ':',
            2,
            StringSplitOptions.TrimEntries);

        if (parts.Length != 2)
            return false;

        if (!Enum.TryParse<TSortField>(
                parts[0],
                ignoreCase: true,
                out var field))
        {
            return false;
        }

        if (!Enum.TryParse<SortDirection>(
                parts[1],
                ignoreCase: true,
                out var direction))
        {
            return false;
        }

        result = new SortField<TSortField>
        {
            Field = field,
            Direction = direction
        };

        return true;
    }
}