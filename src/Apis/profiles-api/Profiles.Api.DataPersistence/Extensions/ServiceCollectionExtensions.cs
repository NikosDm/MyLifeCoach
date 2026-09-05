using Libraries.DataInfrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Profiles.Api.Core.Abstractions.Repositories;
using Profiles.Api.Core.Abstractions.Publishers;
using Profiles.Api.DataPersistence.Context;
using Profiles.Api.DataPersistence.Factories;
using Profiles.Api.DataPersistence.Filters;
using Profiles.Api.DataPersistence.Publishers;
using Profiles.Api.DataPersistence.Repositories;
using Profiles.Api.DataPersistence.Subscribers;

namespace Profiles.Api.DataPersistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabaseContext<ProfileDbContext>(configuration, "ProfilesDB")
            .AddMessaging<ProfileSubscribeFilter>(configuration, "ProfilesDB");

        return services
            .AddRepositories()
            .AddFactories()
            .AddSubscribers()
            .AddPublishers();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IProfileRepository, PersonalProfileRepository>()
            .AddScoped<IProfileRepository, FinancialProfileRepository>()
            .AddScoped<IProfileRepository, ProfessionalProfileRepository>()
            .AddScoped<IProfileRepository, FitnessProfileRepository>();

    private static IServiceCollection AddFactories(this IServiceCollection services)
        => services
            .AddScoped<IProfileRepositoryFactory, ProfileRepositoryFactory>();

    private static IServiceCollection AddSubscribers(this IServiceCollection services)
        => services
            .AddScoped<UserCreatedSubscriber>();

    private static IServiceCollection AddPublishers(this IServiceCollection services)
        => services
            .AddScoped<IUserStatusPublisher, UserStatusPublisher>();
}
