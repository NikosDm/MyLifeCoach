using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Profiles.Api.Domain.Models;
using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.DataPersistence.Context.Configurations;

internal sealed class ProfessionalSkillConfiguration : IEntityTypeConfiguration<ProfessionalSkill>
{
    public void Configure(EntityTypeBuilder<ProfessionalSkill> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ProfileId)
            .IsRequired();

        builder.Property(e => e.Name)
            .IsRequired();

        builder.Property(e => e.YearsOfExperience)
            .IsRequired();

        builder.Property(e => e.Category)
            .IsRequired(false);

        builder.Property(e => e.Rating)
            .IsRequired(false);

        builder.HasOne<ProfessionalProfile>()
            .WithMany(p => p.ProfessionalSkills)
            .HasForeignKey(e => e.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
