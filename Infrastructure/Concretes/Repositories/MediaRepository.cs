using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class MediaRepository : Repository<Media>, IMediaRepository
{
    public MediaRepository(YelloadDbContext context) : base(context) { }
}
