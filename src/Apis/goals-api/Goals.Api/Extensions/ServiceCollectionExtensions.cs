using Goals.Api.Context;
using Libraries.Common.Abstractions;
using Libraries.Common.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goals.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.MapInboundClaims = false;
                options.Authority = configuration.GetValue<string>("Authentication:AuthorityUrl");
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.NameClaimType = "username";
                options.TokenValidationParameters.RoleClaimType = "role";
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("GoalsApiUser", p =>
            {
                p.RequireAuthenticatedUser();
                p.RequireClaim("scope", "goals-api");
            });

            options.AddPolicy("GoalsApiAdmin", p =>
            {
                p.RequireAuthenticatedUser();
                p.RequireClaim("scope", "goals-api");
                p.RequireRole(SecurityConstants.ADMIN_ROLE);
            });
        });

        return services;
    }

    public static IServiceCollection AddUserContext(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, HttpContextUser>();

        return services;
    }
}
