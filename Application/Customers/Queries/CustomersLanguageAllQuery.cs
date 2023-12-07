using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries;

public record CustomersLanguageAllQuery : IRequest<object>;
public class CustomersLanguageAllQueryHandler : IRequestHandler<CustomersLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomersLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(CustomersLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Customer> Customers = await _unitOfWork.CustomerRepository.GetAllAsync()
            ?? throw new NullReferenceException();
        var data = new
        {
            customer_en = Customers.Select(c => new
            {
                c.Id,
                c.ImagePath,
                c.ImageAlt,
                c.Rank
            }).ToList(),
            customer_az=Customers.Select(c => new
            {
                c.Id,
                c.ImagePath,
                ImageAlt = c.ImageAltAz,
                c.Rank
            }).ToList()
        };
        return data;
    }
}