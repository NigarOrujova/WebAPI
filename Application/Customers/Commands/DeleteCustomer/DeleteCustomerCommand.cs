using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(int Id) : IRequest<bool>;

internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer Customer = await _unitOfWork.CustomerRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        await _unitOfWork.CustomerRepository.DeleteAsync(Customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
