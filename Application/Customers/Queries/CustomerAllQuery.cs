using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries;

public record CustomerAllQuery:IRequest<IEnumerable<Customer>>;
public class CustomerAllQueryHandler : IRequestHandler<CustomerAllQuery, IEnumerable<Customer>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Customer>> Handle(CustomerAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Customer> Customers = await _unitOfWork.CustomerRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return Customers;
    }
}
