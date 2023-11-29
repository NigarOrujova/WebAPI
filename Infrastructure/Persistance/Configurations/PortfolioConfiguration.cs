using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Configurations;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.SubTitle)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.Description)
            .HasMaxLength(10000)
            .IsRequired();
        builder.HasIndex(t => t.Slug)
               .IsUnique();
        builder.Property(t => t.Slug)
                .IsRequired();
        builder.HasIndex(t => t.SlugAz)
               .IsUnique();
        builder.Property(t => t.SlugAz)
                .IsRequired();
        builder.Ignore(x => x.CategoryIds); 
        builder.Ignore(x => x.ImageIds);
    }
}