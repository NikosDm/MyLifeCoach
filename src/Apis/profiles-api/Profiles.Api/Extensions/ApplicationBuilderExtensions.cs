using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Profiles.Api.DataPersistence.Context;
using Profiles.Api.Endpoints;

namespace Profiles.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ProfileDbContext>();
        dbContext.Database.Migrate();

        return app;
    }

    public static void UseApiEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        }).AllowAnonymous();
        app.MapPersonalProfileEndpoints();
        app.MapFinancialProfileEndpoints();
        app.MapFitnessProfileEndpoints();
        app.MapProfessionalProfileEndpoints();
        app.MapUserEndpoints();
    }
}
