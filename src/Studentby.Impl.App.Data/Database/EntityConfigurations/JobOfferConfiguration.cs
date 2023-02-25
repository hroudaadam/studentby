using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studentby.App.Data.Database.Entities;

namespace Studentby.Impl.App.Data.Database.EntityConfigurations;

internal class JobOfferConfiguration : BaseEntityConfiguration<JobOffer>
{
    public override void Configure(EntityTypeBuilder<JobOffer> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Title)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(e => e.Spaces)
            .IsRequired();
        builder.Property(e => e.Wage)
            .IsRequired();
        builder.Property(e => e.Start)
            .IsRequired();
        builder.Property(e => e.End)
            .IsRequired();
        builder.Property(e => e.AddressId)
            .IsRequired();
        builder.Property(e => e.GroupId)
            .IsRequired();

        builder.HasOne(e => e.Address)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
