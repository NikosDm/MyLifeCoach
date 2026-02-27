using System;

using DotNetCore.CAP.Filter;

using Libraries.Common.Options;
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

    public static IServiceCollection AddMessaging<T>(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
        where T : SubscribeFilter
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionStringName);
        services
            .AddOptions<CAPOptions>()
            .BindConfiguration("CAPOptions")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IMessageDispatcher, MessageDispatcher>();

        services.AddCap(x =>
        {
            var capOptions = configuration.GetSection("CAPOptions").Get<CAPOptions>();
            x.DefaultGroupName = capOptions.DefaultGroupName;
            x.UsePostgreSql(configuration.GetConnectionString(connectionStringName));
            x.UseRabbitMQ(options =>
            {
                options.HostName = capOptions.RabbitMQ.HostName;
                options.UserName = capOptions.RabbitMQ.UserName;
                options.Password = capOptions.RabbitMQ.Password;
            });
            x.UseDashboard();
        }).AddSubscribeFilter<T>();

        return services;
    }

    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionStringName);
        services
            .AddOptions<CAPOptions>()
            .BindConfiguration("CAPOptions")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IMessageDispatcher, MessageDispatcher>();

        services.AddCap(x =>
        {
            var capOptions = configuration.GetSection("CAPOptions").Get<CAPOptions>();
            x.DefaultGroupName = capOptions.DefaultGroupName;
            x.UsePostgreSql(configuration.GetConnectionString(connectionStringName));
            x.UseRabbitMQ(options =>
            {
                options.HostName = capOptions.RabbitMQ.HostName;
                options.UserName = capOptions.RabbitMQ.UserName;
                options.Password = capOptions.RabbitMQ.Password;
            });
            x.UseDashboard();
        });

        return services;
    }
}