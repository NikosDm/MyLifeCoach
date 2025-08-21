using System;
using Goals.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goals.Api.DataPersistence.Context.Configurations;

internal sealed class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(50);

        builder.Property(e => e.Description)
              .IsRequired(false);

        builder.HasOne(e => e.Type)
              .WithMany()   
              .HasForeignKey(e => e.TypeId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Steps)
              .WithOne(s => s.Goal)
              .HasForeignKey(s => s.GoalId)
              .OnDelete(DeleteBehavior.Cascade);

        // Progress 0..100 with a SQL check constraint (PostgreSQL)
        builder.Property(e => e.Progress)
              .IsRequired();

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_GoalStep_Progress_Range", "\"Progress\" BETWEEN 0 AND 100");
        });

        builder.Property(e => e.Status)
              .HasConversion<int>()
              .IsRequired();
    }
}