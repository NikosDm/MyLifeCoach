using System.Text.Json;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Profile.Api.Domain.Models;
using Profile.Api.Domain.Models.Payloads;

namespace Profile.Api.DataPersistence.Context.Configurations;

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
