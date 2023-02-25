using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studentby.App.Data.Database.Entities;

namespace Studentby.Impl.App.Data.Database.EntityConfigurations;

internal abstract class BaseEntityConfiguration<T>
    : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(typeof(T).Name);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Version)
            .IsRowVersion();
        builder.Property(e => e.Created)
            .IsRequired();
        builder.Property(e => e.Updated)
            .IsRequired();
    }
}
