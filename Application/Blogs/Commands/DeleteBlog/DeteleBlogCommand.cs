using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Blogs.Commands.DeleteBlog;

public record DeteleBlogCommand(int Id) : IRequest<bool>;

internal class DeteleBlogCommandHandler : IRequestHandler<DeteleBlogCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeteleBlogCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeteleBlogCommand request, CancellationToken cancellationToken)
    {
        Blog blog = await _unitOfWork.BlogRepository.GetAsync(n => n.Id == request.Id, includes: new Expression<Func<Blog, object>>[]
            {
                x => x.TagCloud
            })
            ?? throw new NullReferenceException();

        await _unitOfWork.BlogRepository.DeleteAsync(blog);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}