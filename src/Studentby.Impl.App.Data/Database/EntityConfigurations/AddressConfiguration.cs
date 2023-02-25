using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studentby.App.Data.Database.Entities;

namespace Studentby.Impl.App.Data.Database.EntityConfigurations;

internal class AddressConfiguration : BaseEntityConfiguration<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Country)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(e => e.City)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(e => e.Street)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(e => e.Number)
            .HasMaxLength(50)
            .IsRequired();
    }
}
