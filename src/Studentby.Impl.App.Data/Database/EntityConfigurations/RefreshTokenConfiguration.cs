using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studentby.App.Data.Database.Entities;

namespace Studentby.Impl.App.Data.Database.EntityConfigurations;

internal class RefreshTokenConfiguration : BaseEntityConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Token)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.Expiration)
            .IsRequired();
        builder.Property(e => e.Revoked)
            .IsRequired();
        builder.Property(e => e.UserId)
            .IsRequired();
    }
}
