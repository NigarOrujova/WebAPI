using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand : IRequest<Customer>
{
    public int Id { get; init; }
    public string ImageAlt { get; init; }
}
public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer entity = await _unitOfWork.CustomerRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.ImageAlt = request.ImageAlt;

        await _unitOfWork.CustomerRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}