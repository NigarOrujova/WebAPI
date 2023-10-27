using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Portfolios.Commands.UpdatePortfolio;

public record UpdatePortfolioCommand : IRequest<Portfolio>
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? SubTitle { get; init; }
    public string? Description { get; init; }
    public bool IsMain { get; init; }
    public List<int>? CategoryIds { get; set; }
}
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

        entity.Title = request.Title;
        entity.SubTitle = request.SubTitle;
        entity.Description = request.Description;
        entity.IsMain = request.IsMain;
        if (request.CategoryIds != null)
        {
            entity.PortfolioCategories?.RemoveAll(x => !request.CategoryIds.Contains(x.CategoryId));
            foreach (var categoryId in request.CategoryIds.Where(x => !entity.PortfolioCategories.Any(rc => rc.CategoryId == x)))
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