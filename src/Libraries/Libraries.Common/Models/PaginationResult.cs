using System;
using System.Collections.Generic;

namespace Libraries.Common.Models;

public class PaginationResult<T>(
    IReadOnlyCollection<T> items,
    int totalCount,
    int pageNumber,
    int pageSize)
{
    public IReadOnlyCollection<T> Items { get; } = items;
    public int TotalCount { get; } = totalCount;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}