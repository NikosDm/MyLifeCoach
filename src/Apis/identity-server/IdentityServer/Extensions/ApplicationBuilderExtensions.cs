using IdentityServer.DataAccess.Context;

using Libraries.Api.Middleware;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace IdentityServer.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseIdentityServerPipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseExceptionHandler(o => { });

        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("AllowSPA");
        app.UseAuthentication();
        app.UseMiddleware<UserContextMiddleware>();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }

    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}
