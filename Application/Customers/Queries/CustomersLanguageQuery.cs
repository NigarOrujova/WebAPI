using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries;

public record CustomersLanguageQuery(int Id) : IRequest<object>;
internal class CustomersLanguageQueryHandler : IRequestHandler<CustomersLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomersLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<object> Handle(CustomersLanguageQuery request, CancellationToken cancellationToken)
    {
        Customer entity = await _unitOfWork.CustomerRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();
        var data = new
        {
            customer_en = new
            {
                entity.ImagePath,
                entity.ImageAlt,
                entity.Rank
            },
            customer_az = new
            {
                entity.ImagePath,
                ImageAlt=entity.ImageAltAz,
                entity.Rank
            }
        };
        return data;
    }
}