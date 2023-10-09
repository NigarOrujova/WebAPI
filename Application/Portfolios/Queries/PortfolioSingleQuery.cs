using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Portfolios.Queries;

public record PortfolioSingleQuery(int Id) : IRequest<Portfolio>;

internal class PortfolioSingleQueryHandler : IRequestHandler<PortfolioSingleQuery, Portfolio>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Portfolio> Handle(PortfolioSingleQuery request, CancellationToken cancellationToken)
    {
        Portfolio entity = await _unitOfWork.PortfolioRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}