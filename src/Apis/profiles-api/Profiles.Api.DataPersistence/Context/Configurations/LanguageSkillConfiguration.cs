using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Profiles.Api.Domain.Models;
using Profiles.Api.Domain.Models.RatedItems;

namespace Profiles.Api.DataPersistence.Context.Configurations;

internal sealed class LanguageSkillConfiguration : IEntityTypeConfiguration<LanguageSkill>
{
    public void Configure(EntityTypeBuilder<LanguageSkill> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ProfileId)
            .IsRequired();

        builder.Property(e => e.LanguageCode)
            .IsRequired();

        builder.Property(e => e.IsNative)
            .IsRequired(false);

        builder.Property(e => e.Rating)
            .IsRequired(false);

        builder.HasOne<PersonalProfile>()
            .WithMany(p => p.LanguageSkills)
            .HasForeignKey(e => e.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
