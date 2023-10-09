using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.Property(t => t.FulllName)
            .HasMaxLength(200)
            .IsRequired();
        builder.Ignore(t => t.Image);
        builder.Ignore(t => t.Image2);
    }
}