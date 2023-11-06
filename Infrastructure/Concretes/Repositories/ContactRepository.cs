using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class ContactRepository:Repository<Contact>,IContactRepository
{
    public ContactRepository(YelloadDbContext context):base(context) { }
}
