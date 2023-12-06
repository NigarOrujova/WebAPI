using Infrastructure.Concretes.Common;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Roles.Commands;

public class RoleSortCommand : IRequest<JsonResponse>
{
    public int EntityId { get; set; }
    public byte NewRank { get; set; }


    public class RoleSortCommandHandler : IRequestHandler<RoleSortCommand, JsonResponse>
    {
        private readonly YelloadDbContext db;

        public RoleSortCommandHandler(YelloadDbContext db)
        {
            this.db = db;
        }

        public async Task<JsonResponse> Handle(RoleSortCommand request, CancellationToken cancellationToken)
        {
            var movedRoleEntry = await db.Roles.FirstOrDefaultAsync(m => m.Id == request.EntityId, cancellationToken);

            if (movedRoleEntry != null)
            {
                // Update the rank of the moved entity
                movedRoleEntry.Rank = request.NewRank;

                // Adjust the ranks of other entities
                var otherRoles = await db.Roles
                    .Where(m => m.Id != request.EntityId && m.Rank <= request.NewRank)
                    .OrderByDescending(m => m.Rank)
                    .ToListAsync(cancellationToken);

                foreach (var item in otherRoles)
                {
                    item.Rank = request.NewRank >= 1 ? --request.NewRank : request.NewRank;
                }

                await db.SaveChangesAsync(cancellationToken);

                return new JsonResponse
                {
                    Error = false,
                    Message = "Sorted!"
                };
            }

            return new JsonResponse
            {
                Error = true,
                Message = "Entity not found!"
            };
        }
    }
}
