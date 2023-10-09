using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest<bool>;

internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category Category = await _unitOfWork.CategoryRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        await _unitOfWork.CategoryRepository.DeleteAsync(Category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
