using System;

namespace Profiles.Api.Domain.Abstractions;

public interface IRatedItem
{
    int? Rating { get; }
    Guid ProfileId { get; }
}
