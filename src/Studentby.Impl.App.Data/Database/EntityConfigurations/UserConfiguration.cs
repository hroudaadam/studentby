using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studentby.App.Data.Database.Entities;

namespace Studentby.Impl.App.Data.Database.EntityConfigurations;

internal class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.PasswordHash)
            .IsRequired();
        builder.Property(e => e.PasswordSalt)
            .IsRequired();
        builder.Property(e => e.Role)
            .IsRequired();


        builder.HasIndex(e => e.Email).IsUnique();
    }
}
