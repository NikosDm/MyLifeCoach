using Microsoft.EntityFrameworkCore;

namespace Goals.Api.DataPersistence.Context;

public sealed class GoalsDbContext(DbContextOptions<GoalsDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GoalsDbContext).Assembly);
    }
}