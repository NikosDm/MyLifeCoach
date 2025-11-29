using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Profiles.Api.Domain.Models;
using Profiles.Api.Domain.Models.Payloads;

namespace Profiles.Api.DataPersistence.Context.Configurations;

internal sealed class ProfessionalProfileConfiguration : BaseProfileConfiguration<ProfessionalProfile>
{
    public override void Configure(EntityTypeBuilder<ProfessionalProfile> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Payload)
            .HasConversion(
                v => JsonSerializer.Serialize(v, null as JsonSerializerOptions),
                v => JsonSerializer.Deserialize<ProfessionalProfilePayload>(v, null as JsonSerializerOptions))
            .IsRequired();

        builder.HasMany(e => e.ProfessionalSkills)
            .WithOne()
            .HasForeignKey(s => s.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
