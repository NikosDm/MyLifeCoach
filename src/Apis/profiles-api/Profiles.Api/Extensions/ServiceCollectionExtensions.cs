using System;

using Libraries.Api.Exceptions;
using Libraries.Api.Extensions;
using Libraries.Common.Constants;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Profiles.Api.Constants;

namespace Profiles.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApiAuthentication(configuration)
            .AddAuthorizationBuilder()
            .AddPolicy(ApiConstants.ProfileApiUserPolicy, p =>
            {
                p.RequireAuthenticatedUser();
                p.RequireClaim("scope", "profiles-api");
            })
            .AddPolicy(ApiConstants.ProfileApiAdminPolicy, p =>
            {
                p.RequireAuthenticatedUser();
                p.RequireClaim("scope", "profiles-api");
                p.RequireRole(SecurityConstants.ADMIN_ROLE);
            });

        return services;
    }

    public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(static c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Profile API",
                Version = "v1",
                Description = "Minimal API for managing users' profiles."
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Paste ONLY the JWT. Swagger will add 'Bearer ' automatically."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<ApiExceptionHandler>();
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("ProfilesDB"));

        return services;
    }
}
