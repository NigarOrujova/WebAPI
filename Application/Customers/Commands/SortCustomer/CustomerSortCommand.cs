using Application.Abstracts.Common.Interfaces;
using Application.Customers.Commands.SortCustomer;
using Domain.Dtos;
using MediatR;

namespace Application.Customers.Commands.SortCustomer;

public class CustomerSortCommand : IRequest<JsonResponse>
{
    public int EntityId { get; set; }
    public byte NewRank { get; set; }


    public class RoleSortCommandHandler : IRequestHandler<CustomerSortCommand, JsonResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleSortCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<JsonResponse> Handle(CustomerSortCommand request, CancellationToken cancellationToken)
        {
            var movedRoleEntry = await _unitOfWork.CustomerRepository.GetAsync(m => m.Id == request.EntityId);

            if (movedRoleEntry != null)
            {
                // Update the rank of the moved entity
                movedRoleEntry.Rank = request.NewRank;

                // Adjust the ranks of other entities
                var otherRoles = await _unitOfWork.CustomerRepository
                    .GetAllAsync(m => m.Id != request.EntityId && m.Rank <= request.NewRank);

                foreach (var item in otherRoles)
                {
                    item.Rank = request.NewRank >= 1 ? --request.NewRank : request.NewRank;
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new JsonResponse
                {
                    Message = "Sorted!"
                };
            }

            return new JsonResponse
            {
                Message = "Entity not found!"
            };
        }
    }
}