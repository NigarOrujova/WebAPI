using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistance.Configurations;

public class OurValueConfiguration : IEntityTypeConfiguration<OurValue>
{
    public void Configure(EntityTypeBuilder<OurValue> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired(false);
        builder.Property(t => t.Description)
            .HasMaxLength(1000)
            .IsRequired(false);
    }
}
