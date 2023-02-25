using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studentby.App.Data.Database.Entities;

namespace Studentby.Impl.App.Data.Database.EntityConfigurations;

internal class GroupConfiguration : BaseEntityConfiguration<Group>
{
    public override void Configure(EntityTypeBuilder<Group> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}
