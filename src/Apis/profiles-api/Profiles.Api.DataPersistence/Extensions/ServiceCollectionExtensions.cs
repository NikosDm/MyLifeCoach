using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profiles.Api.Core.Abstractions;
using Profiles.Api.DataPersistence.Context;
using Profiles.Api.DataPersistence.Factories;
using Profiles.Api.DataPersistence.Repositories;

namespace Profiles.Api.DataPersistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProfileDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("ProfilesDB")));

        return services
            .AddRepositories()
            .AddFactories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddScoped<IProfileRepository, PersonalProfileRepository>()
            .AddScoped<IProfileRepository, FinancialProfileRepository>()
            .AddScoped<IProfileRepository, ProfessionalProfileRepository>()
            .AddScoped<IProfileRepository, FitnessProfileRepository>();

    private static IServiceCollection AddFactories(this IServiceCollection services)
        => services
            .AddScoped<IProfileRepositoryFactory, ProfileRepositoryFactory>();
}
