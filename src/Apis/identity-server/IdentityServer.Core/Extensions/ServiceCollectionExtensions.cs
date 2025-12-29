using IdentityServer.Core.Abstractions;
using IdentityServer.Core.Services;

using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServerCore(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}