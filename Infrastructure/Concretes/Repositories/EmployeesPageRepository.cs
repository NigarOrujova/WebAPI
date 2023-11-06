using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class EmployeesPageRepository:Repository<EmployeesPage>,IEmployeesPageRepository
{
    public EmployeesPageRepository(YelloadDbContext context) : base(context) { }
}
