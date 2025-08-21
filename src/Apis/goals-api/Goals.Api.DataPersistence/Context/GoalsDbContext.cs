using Goals.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Goals.Api.DataPersistence.Context;

public sealed class GoalsDbContext(DbContextOptions<GoalsDbContext> options) : DbContext(options)
{
    public DbSet<Goal> Goals { get; set; }
    public DbSet<GoalType> GoalTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GoalsDbContext).Assembly);
    }
}