using Microsoft.EntityFrameworkCore;

namespace Profile.Api.DataPersistence.Context;

public sealed class ProfileDbContext(DbContextOptions<ProfileDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfileDbContext).Assembly);
    }
}
