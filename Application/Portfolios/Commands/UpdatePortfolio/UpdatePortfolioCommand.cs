using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Portfolios.Commands.UpdatePortfolio;

public record UpdatePortfolioCommand(int Id,Portfolio Portfolio) : IRequest<Portfolio>;
public class UpdatePortfolioCommandHandler : IRequestHandler<UpdatePortfolioCommand, Portfolio>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePortfolioCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Portfolio> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
    {
        Portfolio entity = await _unitOfWork.PortfolioRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.Title = request.Portfolio.Title;
        entity.SubTitle = request.Portfolio.SubTitle;
        entity.Description = request.Portfolio.Description;
        entity.IsMain = request.Portfolio.IsMain;
        if (request.Portfolio.CategoryIds != null)
        {
            entity.PortfolioCategories?.RemoveAll(x => !request.Portfolio.CategoryIds.Contains(x.CategoryId));
            foreach (var categoryId in request.Portfolio.CategoryIds.Where(x => !entity.PortfolioCategories.Any(rc => rc.CategoryId == x)))
            {
                PortfolioCategory portfolioCategory = new PortfolioCategory
                {
                    PortfolioId = request.Id,
                    CategoryId = categoryId
                };
                entity.PortfolioCategories.Add(portfolioCategory);
            }
        }
        await _unitOfWork.PortfolioRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}