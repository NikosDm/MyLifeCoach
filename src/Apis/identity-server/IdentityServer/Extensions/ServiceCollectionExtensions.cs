using IdentityServer.DataAccess.Entities;
using IdentityServer.DataAccess.Extensions;
using IdentityServer.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRazorPages();

        services.AddIdentityDb(configuration);

        services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.IssuerUri = configuration.GetValue<string>("Authentication:AuthorityUrl");
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<UserProfileService>();

        services.ConfigureApplicationCookie(options => 
        {
            options.Cookie.SameSite = SameSiteMode.Lax;
        });

        services.AddAuthentication();

        return services;
    }
}
