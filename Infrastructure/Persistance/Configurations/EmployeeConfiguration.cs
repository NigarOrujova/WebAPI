using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeesPage>
{
    public void Configure(EntityTypeBuilder<EmployeesPage> builder)
    {
        builder.Ignore(t => t.Image);
        builder.Ignore(t => t.Image2);
    }
}
