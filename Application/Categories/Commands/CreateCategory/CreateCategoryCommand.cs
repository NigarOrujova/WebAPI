using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<int>
{
    public string Name { get; init; } = null!;
    public List<int>? PortfolioIds { get; set; } = new List<int>();
}
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category();

        entity.Name = request.Name;
        if (request.PortfolioIds != null)
        {
            entity.PortfolioCategories = new List<PortfolioCategory>();
            foreach (var id in request.PortfolioIds)
            {
                PortfolioCategory portfolioCategory = new PortfolioCategory()
                {
                    PortfolioId = id,
                    Category = entity
                };
                entity.PortfolioCategories.Add(portfolioCategory);
            }
        }

        await _unitOfWork.CategoryRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}