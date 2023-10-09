using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries;

public record CustomerSingleQuery(int Id) : IRequest<Customer>;

internal class CustomerSingleQueryHandler : IRequestHandler<CustomerSingleQuery, Customer>
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Customer> Handle(CustomerSingleQuery request, CancellationToken cancellationToken)
    {
        Customer entity = await _unitOfWork.CustomerRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}