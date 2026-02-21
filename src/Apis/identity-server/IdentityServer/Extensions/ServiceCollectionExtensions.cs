using Duende.IdentityServer.Models;

using IdentityServer.Core.Extensions;
using IdentityServer.DataAccess.Entities;
using IdentityServer.DataAccess.Extensions;
using IdentityServer.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRazorPages();

        // Add CORS to allow React SPA
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSPA", policy =>
            {
                var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins")
                    .Get<string[]>() ?? ["http://localhost:9000", "https://localhost:3000"];

                policy
                    .WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        services
            .AddIdentityServerCore()
            .AddIdentityConfiguration(configuration)
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.IssuerUri = configuration.GetValue<string>("Authentication:AuthorityUrl");
            })
            .AddInMemoryIdentityResources([
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            ])
            // TODO: This probably needs to change. Secrets from configuration are hashed. 
            // We should do something so that secrets are not hashed there but here.
            .AddInMemoryApiScopes(configuration.GetSection("IdentityServer:ApiScopes"))
            .AddInMemoryClients(configuration.GetSection("IdentityServer:Clients"))
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<UserProfileService>();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        });

        services.AddAuthentication();

        return services;
    }
}
