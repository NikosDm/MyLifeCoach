using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Profiles.Api.Domain.Models;
using Profiles.Api.Domain.Models.Payloads;

using System.Text.Json;

namespace Profiles.Api.DataPersistence.Context.Configurations;

internal sealed class PersonalProfileConfiguration : BaseProfileConfiguration<PersonalProfile>
{
    public override void Configure(EntityTypeBuilder<PersonalProfile> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Payload)
            .HasConversion(
                v => JsonSerializer.Serialize(v, null as JsonSerializerOptions),
                v => JsonSerializer.Deserialize<PersonalProfilePayload>(v, null as JsonSerializerOptions))
            .IsRequired();

        builder.HasMany(e => e.LanguageSkills)
            .WithOne()
            .HasForeignKey(s => s.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
