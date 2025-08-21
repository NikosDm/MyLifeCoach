using Goals.Api.Domain.Entities;
using Goals.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goals.Api.DataPersistence.Context.Configurations;

internal sealed class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ComplexProperty(
            o => o.Name, nameBuilder =>
            {
                nameBuilder.IsRequired();
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Goal.Name))
                    .IsRequired()
                    .HasMaxLength(50);
            });

        builder.Property(e => e.Description)
              .IsRequired(false);

        builder.HasOne<GoalType>()
              .WithMany()
              .HasForeignKey(e => e.TypeId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.ComplexProperty(
            o => o.Period, nameBuilder =>
            {
                nameBuilder.IsRequired();
                nameBuilder.Property(n => n.StartDate)
                    .HasColumnName(nameof(GoalPeriod.StartDate))
                    .IsRequired();

                nameBuilder.Property(n => n.EndDate)
                    .HasColumnName(nameof(GoalPeriod.EndDate))
                    .IsRequired(false);
            });

        builder.ComplexProperty(
            o => o.Progress, nameBuilder =>
            {
                nameBuilder.IsRequired();
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Goal.Progress))
                    .IsRequired();
            });

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_GoalStep_Progress_Range", "\"Progress\" BETWEEN 0 AND 100");
        });

        builder.Property(e => e.Status)
            .HasConversion<int>()
            .IsRequired();
            
        builder.HasMany(e => e.Steps)
            .WithOne()
            .HasForeignKey(s => s.GoalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
