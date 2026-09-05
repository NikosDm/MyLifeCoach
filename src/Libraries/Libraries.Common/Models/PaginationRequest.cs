using System;

namespace Libraries.Common.Models;

public record PaginationRequest<TRequest, TSortField>(
    TRequest Request,
    int PageNumber,
    int PageSize,
    SortField<TSortField>[] SortFields = null)
    where TRequest : class
    where TSortField : struct, Enum
{
    public TRequest Request { get; } = Request;
    public int PageNumber { get; } = PageNumber;
    public int PageSize { get; } = PageSize;
    public SortField<TSortField>[] SortFields { get; } = SortFields ?? [];
}