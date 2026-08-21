using IdentityServer.Core.Abstractions;
using IdentityServer.Core.Services;
using IdentityServer.Core.Subscribers;

using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServerCore(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddSubscribers();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAccountService, AccountService>();

        return services;
    }

    private static IServiceCollection AddSubscribers(this IServiceCollection services)
    {
        services
            .AddScoped<UserActivationChangedSubscriber>();

        return services;
    }
}