using System;

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
}