using Libraries.Common.Entities;

namespace Libraries.Common.Abstractions;

public interface IUserContextAccessor
{
    UserContextData Current { get; set; }
}