using System;

namespace Profile.Api.Domain.Abstractions;

public interface IRatedItem
{
    int? Rating { get; }
    Guid ProfileId { get; }
}
