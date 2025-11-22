using Microsoft.Extensions.DependencyInjection;

using Libraries.Api.Exceptions;
using Libraries.Common.Abstractions;
using Libraries.Api.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Libraries.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ApiExceptionHandler>();
        return services;
    }

    public static IServiceCollection AddHttpUserContext(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, HttptUserContext>();
        return services;
    }

    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
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
        return services;
    }
}
