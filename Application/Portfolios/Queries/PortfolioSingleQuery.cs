using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Portfolios.Queries;

public record PortfolioSingleQuery: IRequest<object>
{
    public int Id { get; set; }
    public string? Slug { get; init; }

}

public class PortfolioSingleQueryHandler : IRequestHandler<PortfolioSingleQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(PortfolioSingleQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Slug))
        {
            if (await _unitOfWork.PortfolioRepository.IsExistAsync(x=>x.Slug==request.Slug))
            {
                return await _unitOfWork.PortfolioRepository.GetPortfolioBySlugAsync(request.Slug);
            }
            else
            {
                throw new InvalidOperationException("slug is null");
            }
        } 
        Portfolio entity = await _unitOfWork.PortfolioRepository.GetAsync(n => n.Id == request.Id,
            includes: new Expression<Func<Portfolio, object>>[]
            {
                x => x.PortfolioCategories,
                x => x.Images
            })
            ?? throw new InvalidOperationException("Portfolio entity is null");

        IEnumerable<Category> categories = await _unitOfWork.CategoryRepository.GetAllAsync(includes: x => x.PortfolioCategories)
          ?? throw new InvalidOperationException("Categories is null");

        return entity;
    }
}