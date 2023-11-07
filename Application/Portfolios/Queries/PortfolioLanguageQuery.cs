using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Portfolios.Queries;

public record PortfolioLanguageQuery(int Id):IRequest<object>;
internal class PortfolioLanguageQueryHandler : IRequestHandler<PortfolioLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(PortfolioLanguageQuery request, CancellationToken cancellationToken)
    {
        Portfolio entity = await _unitOfWork.PortfolioRepository.GetAsync(n => n.Id == request.Id,
            includes: new Expression<Func<Portfolio, object>>[]
            {
                x => x.PortfolioCategories,
                x => x.Images
            })
            ?? throw new NullReferenceException();

        return entity;
    }
}