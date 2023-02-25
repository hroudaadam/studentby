using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studentby.App.Data.Database.Entities;

namespace Studentby.Impl.App.Data.Database.EntityConfigurations;

internal class StudentConfiguration : BaseEntityConfiguration<Student>
{
    public override void Configure(EntityTypeBuilder<Student> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(e => e.LastName)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(e => e.DateOfBirth)
            .IsRequired();
        builder.Property(e => e.AddressId)
            .IsRequired();
        builder.Property(e => e.UserId)
            .IsRequired();
    }
}
