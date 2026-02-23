using Goals.Api.DataPersistence.Context;
using Goals.Api.Endpoints;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Goals.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<GoalsDbContext>();
        dbContext.Database.Migrate();

        return app;
    }

    public static void UseApiEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        }).AllowAnonymous();
        app.MapGoalTypeEndpoints();
        app.MapGoalEndpoints();
        app.MapGoalStepEndpoints();
    }
}
