using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studentby.App.Data.Database.Entities;

namespace Studentby.Impl.App.Data.Database.EntityConfigurations;

internal class JobApplicationConfiguration : BaseEntityConfiguration<JobApplication>
{
    public override void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.State)
            .IsRequired();
        builder.Property(e => e.JobOfferId)
            .IsRequired();
        builder.Property(e => e.StudentId)
            .IsRequired();
    }
}
