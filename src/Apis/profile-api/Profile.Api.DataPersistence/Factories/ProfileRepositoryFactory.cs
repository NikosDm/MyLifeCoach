using System;
using System.Collections.Generic;
using System.Linq;
using Profile.Api.Core.Abstractions;
using Profile.Api.Domain.Abstractions;
using Profile.Api.Domain.Enums;

namespace Profile.Api.DataPersistence.Factories;

internal sealed class ProfileRepositoryFactory(IEnumerable<IProfileRepository> repos) : IProfileRepositoryFactory
{
    private readonly IReadOnlyDictionary<ProfileType, IProfileRepository> _reposMap = repos.ToDictionary(r => r.Handles);

    public IProfileRepository<T> Get<T>(ProfileType type) where T : ProfileBase
    {
        var typedRepo = _reposMap.TryGetValue(type, out var repo)
           ? repo : throw new KeyNotFoundException($"No repo for {type}.");

        return typedRepo is IProfileRepository<T> typed
           ? typed
           : throw new InvalidOperationException(
               $"Repo for {type} doesn't implement IProfileRepository<{typeof(T).Name}>.");
    }
}
