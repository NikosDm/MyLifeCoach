using Libraries.Api.Extensions;
using Libraries.Common.Constants;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goals.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApiAuthentication(configuration)
            .AddAuthorizationBuilder()
            .AddPolicy("GoalsApiUser", p =>
            {
                p.RequireAuthenticatedUser();
                p.RequireClaim("scope", "goals-api");
            })
            .AddPolicy("GoalsApiAdmin", p =>
            {
                p.RequireAuthenticatedUser();
                p.RequireClaim("scope", "goals-api");
                p.RequireRole(SecurityConstants.ADMIN_ROLE);
            });

        return services;
    }
}
