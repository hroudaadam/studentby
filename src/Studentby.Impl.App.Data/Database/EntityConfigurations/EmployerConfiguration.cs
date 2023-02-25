using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studentby.App.Data.Database.Entities;

namespace Studentby.Impl.App.Data.Database.EntityConfigurations;

internal class EmployerConfiguration : BaseEntityConfiguration<Employer>
{
    public override void Configure(EntityTypeBuilder<Employer> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.JobTitle)
            .HasMaxLength(50);
        builder.Property(e => e.UserId)
            .IsRequired();
        builder.Property(e => e.GroupId)
            .IsRequired();
    }
}
