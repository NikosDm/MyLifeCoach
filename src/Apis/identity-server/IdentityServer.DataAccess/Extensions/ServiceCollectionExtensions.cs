using IdentityServer.DataAccess.Context;
using IdentityServer.DataAccess.Entities;

using Libraries.DataInfrastructure.Extensions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabaseContext<UsersDbContext>(configuration, "IdentityDB")
            .AddMessaging(configuration, "IdentityDB")
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<UsersDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
