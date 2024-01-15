using Application.Abstracts.Common.Interfaces;
using Domain.Dtos;
using MediatR;

namespace Application.Teams.Commands.SortTeam;

public class TeamSortCommand: IRequest<JsonResponse>
{
    public int EntityId { get; set; }
    public int AboveEntityId { get; set; }


    public class RoleSortCommandHandler : IRequestHandler<TeamSortCommand, JsonResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleSortCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<JsonResponse> Handle(TeamSortCommand request, CancellationToken cancellationToken)
        {
            var firstRoleEntry = await _unitOfWork.TeamRepository.GetAsync(m => m.Id == request.AboveEntityId);

            byte rank = (byte)firstRoleEntry.Rank;

            var movedRoleEntry = await _unitOfWork.TeamRepository.GetAsync(m => m.Id == request.EntityId);

            movedRoleEntry.Rank = Convert.ToByte(rank >= 1 ? --rank : rank);

            rank = (byte)movedRoleEntry.Rank;

            var anotherRoles = await _unitOfWork.TeamRepository
                    .GetAllAsync(m => m.Id != request.AboveEntityId && m.Id != request.EntityId && m.Rank <= rank);

            foreach (var item in anotherRoles)
            {
                item.Rank = rank >= 1 ? --rank : rank;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new JsonResponse
            {
                Status = "false",
                Message = "Sorted!"
            };
        }
    }
}