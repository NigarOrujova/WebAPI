using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand : IRequest<Category>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int? PortfolioId { get; init; }
    public List<int>? PortfolioIds { get; set; }
}
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category entity = await _unitOfWork.CategoryRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.Name = request.Name;
        if (request.PortfolioIds != null)
        {
            entity.PortfolioCategories?.RemoveAll(x => !request.PortfolioIds.Contains(x.CategoryId));
            foreach (var portfolioId in request.PortfolioIds.Where(x => !entity.PortfolioCategories.Any(rc => rc.CategoryId == x)))
            {
                PortfolioCategory portfolioCategory = new PortfolioCategory
                {
                    CategoryId = request.Id,
                    PortfolioId = portfolioId
                };
                entity.PortfolioCategories.Add(portfolioCategory);
            }
        }

        await _unitOfWork.CategoryRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}