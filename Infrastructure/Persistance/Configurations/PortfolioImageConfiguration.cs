using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Configurations;

public class PortfolioImageConfiguration : IEntityTypeConfiguration<PortfolioImage>
{
    public void Configure(EntityTypeBuilder<PortfolioImage> builder)
    {
        builder.Ignore(t => t.Image);
    }
}