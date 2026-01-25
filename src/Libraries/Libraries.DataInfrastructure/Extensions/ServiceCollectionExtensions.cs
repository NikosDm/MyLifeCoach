using System;

using Libraries.DataInfrastructure.Abstractions;
using Libraries.DataInfrastructure.Messages;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Libraries.DataInfrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseContext<TContext>(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
        where TContext : DbContext
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionStringName);
        services.AddDbContext<TContext>(options => options.UseNpgsql(configuration.GetConnectionString(connectionStringName)));

        return services;
    }

    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionStringName);
        services.AddScoped<IMessageDispatcher, MessageDispatcher>();

        services.AddCap(x =>
        {
            x.UsePostgreSql(configuration.GetConnectionString(connectionStringName));
            x.UseRabbitMQ(options =>
            {
                options.HostName = configuration["RabbitMQ:HostName"];
                options.UserName = configuration["RabbitMQ:UserName"];
                options.Password = configuration["RabbitMQ:Password"];
            });
            x.UseDashboard();
        });

        return services;
    }
}