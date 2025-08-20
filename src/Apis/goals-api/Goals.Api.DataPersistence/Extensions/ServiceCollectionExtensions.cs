using Goals.Api.Core.Abstractions.Repositories;
using Goals.Api.DataPersistence.Context;
using Goals.Api.DataPersistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goals.Api.DataPersistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GoalsDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("GoalsDB")));

        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddScoped<IGoalRepository, GoalRepository>()
            .AddScoped<IGoalTypeRepository, GoalTypeRepository>()
            .AddScoped<IGoalStepRepository, GoalStepRepository>();
}
