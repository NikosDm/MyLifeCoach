using System.Text.Json;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Profiles.Api.Domain.Models;
using Profiles.Api.Domain.Models.Payloads;

namespace Profiles.Api.DataPersistence.Context.Configurations;

internal sealed class FinancialProfileConfiguration : BaseProfileConfiguration<FinancialProfile>
{
    public override void Configure(EntityTypeBuilder<FinancialProfile> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Payload)
            .HasConversion(
                v => JsonSerializer.Serialize(v, null as JsonSerializerOptions),
                v => JsonSerializer.Deserialize<FinancialProfilePayload>(v, null as JsonSerializerOptions))
            .IsRequired();
    }
}
