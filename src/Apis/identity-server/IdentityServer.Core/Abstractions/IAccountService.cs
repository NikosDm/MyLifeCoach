using System.Threading.Tasks;

using IdentityServer.Core.Dtos.Requests;
using IdentityServer.Core.Dtos.Responses;

namespace IdentityServer.Core.Abstractions;

public interface IAccountService
{
    Task<RegisterResponse> CreateAsync(CreateUserRequest request);
    Task<LoginResponse> LoginAsync(LoginUserRequest request);
}