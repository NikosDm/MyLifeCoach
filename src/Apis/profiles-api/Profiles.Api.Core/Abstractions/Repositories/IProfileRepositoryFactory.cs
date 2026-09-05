using Profiles.Api.Domain.Abstractions;
using Profiles.Api.Domain.Enums;

namespace Profiles.Api.Core.Abstractions.Repositories;

public interface IProfileRepositoryFactory
{
    IProfileRepository<T> Get<T>(ProfileType type) where T : ProfileBase;
}
