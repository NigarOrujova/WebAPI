using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class FooterConfiguration:IEntityTypeConfiguration<Footer>
{
    public void Configure(EntityTypeBuilder<Footer> builder)
    {
        builder.Property(t => t.Phone)
            .HasMaxLength(20);
        builder.Property(t => t.Email)
           .HasMaxLength(50);
        builder.Property(t => t.Address)
          .HasMaxLength(100);
    }
}