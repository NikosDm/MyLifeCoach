using Goals.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goals.Api.DataPersistence.Context.Configurations;

internal sealed class GoalStepConfiguration : IEntityTypeConfiguration<GoalStep>
{
    public void Configure(EntityTypeBuilder<GoalStep> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(50);

        builder.Property(e => e.Description)
              .IsRequired(false);

        builder.Property(e => e.Order)
              .IsRequired();

        builder.Property(e => e.Progress)
              .IsRequired();

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_GoalStep_Progress_Range", "\"Progress\" BETWEEN 0 AND 100");
        });

        builder.Property(e => e.DueDate)
              .IsRequired(false);

        builder.Property(e => e.Status)
              .HasConversion<int>()
              .IsRequired();
              
        builder.HasOne(e => e.Goal)
              .WithMany(g => g.Steps)
              .HasForeignKey(e => e.GoalId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
