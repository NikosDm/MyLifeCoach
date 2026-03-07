using System;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Profiles.Api.Domain.Models;
using Profiles.Api.Domain.Models.Payloads;

namespace Profiles.Api.DataPersistence.Context.Configurations;

internal sealed class FitnessProfileConfiguration : BaseProfileConfiguration<FitnessProfile>
{
    public override void Configure(EntityTypeBuilder<FitnessProfile> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Payload)
            .HasConversion(
                v => JsonSerializer.Serialize(v, null as JsonSerializerOptions),
                v => JsonSerializer.Deserialize<FitnessProfilePayload>(v, null as JsonSerializerOptions))
            .IsRequired();

        builder
            .HasOne(e => e.User)
            .WithOne(e => e.FitnessProfile)
            .HasForeignKey<FitnessProfile>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
