using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Profile.Api.Domain.Abstractions;

namespace Profile.Api.DataPersistence.Context.Configurations;

internal class BaseProfileConfiguration<T> : IEntityTypeConfiguration<T>
    where T : ProfileBase
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.HasIndex(e => e.UserId)
            .IsUnique();
    }
}
