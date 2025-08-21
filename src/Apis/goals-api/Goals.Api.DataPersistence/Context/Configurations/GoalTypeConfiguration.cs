using Goals.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goals.Api.DataPersistence.Context.Configurations;

internal sealed class GoalTypeConfiguration : IEntityTypeConfiguration<GoalType>
{
    public void Configure(EntityTypeBuilder<GoalType> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ComplexProperty(
            o => o.Name, nameBuilder =>
            {
                nameBuilder.IsRequired();
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(GoalType.Name))
                    .IsRequired()
                    .HasMaxLength(50);
            });

        builder.Property(e => e.Description)
            .IsRequired(false);
    }
}
