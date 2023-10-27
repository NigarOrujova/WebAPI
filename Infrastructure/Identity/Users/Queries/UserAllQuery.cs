using Domain.Entities.Membership;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Users.Queries;

public record UserAllQuery : IRequest<IEnumerable<AppUser>>;
public class UserAllQueryHandler : IRequestHandler<UserAllQuery, IEnumerable<AppUser>>
{
    private readonly YelloadDbContext db;

    public UserAllQueryHandler(YelloadDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<AppUser>> Handle(UserAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<AppUser> users = await db.Users.ToListAsync()
               ?? throw new NullReferenceException();
        return users;
    }
}