using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries;

public class CustomersWithPaginationQuery:IRequest<List<Customer>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
public class CustomersWithPaginationQueryHandler:IRequestHandler<CustomersWithPaginationQuery, List<Customer>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomersWithPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Customer>> Handle(CustomersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return (List<Customer>)await _unitOfWork.CustomerRepository.GetPaginatedAsync(request.Page,request.Size);
    }
}
