using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(int Id,Category Category) : IRequest<Category>;
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

        entity.Name = request.Category.Name;
        entity.NameAz = request.Category.NameAz;
        if (request.Category.PortfolioIds != null)
        {
            if (entity.PortfolioCategories == null)
            {
                entity.PortfolioCategories = new List<PortfolioCategory>();
            }
            entity.PortfolioCategories?.RemoveAll(x => !request.Category.PortfolioIds.Contains(x.PortfolioId));
            foreach (var portfolioId in request.Category.PortfolioIds.Where(x => !entity.PortfolioCategories.Any(rc => rc.PortfolioId == x)))
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