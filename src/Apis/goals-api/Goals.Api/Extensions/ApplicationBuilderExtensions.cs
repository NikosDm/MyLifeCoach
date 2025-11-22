using Goals.Api.DataPersistence.Context;
using Goals.Api.Endpoints;

using Microsoft.AspNetCore.Builder;
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
        app.MapGoalTypeEndpoints();
        app.MapGoalEndpoints();
        app.MapGoalStepEndpoints();
    }
}
