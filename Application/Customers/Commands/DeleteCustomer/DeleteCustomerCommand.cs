using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(int Id) : IRequest<bool>;

internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer Customer = await _unitOfWork.CustomerRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        if(Customer.ImagePath != null) 
        {
            _env.ArchiveImage(Customer.ImagePath);
        }
        await _unitOfWork.CustomerRepository.DeleteAsync(Customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
