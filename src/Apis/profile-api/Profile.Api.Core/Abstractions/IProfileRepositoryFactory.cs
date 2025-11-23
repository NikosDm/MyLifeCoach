using Profile.Api.Domain.Abstractions;
using Profile.Api.Domain.Enums;

namespace Profile.Api.Core.Abstractions;

public interface IProfileRepositoryFactory
{
    IProfileRepository<T> Get<T>(ProfileType type) where T : ProfileBase;
}
