using Goals.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goals.Api.DataPersistence.Context.Configurations;

internal sealed class GoalTypeConfiguration : IEntityTypeConfiguration<GoalType>
{
    public void Configure(EntityTypeBuilder<GoalType> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(50);
    }
}
