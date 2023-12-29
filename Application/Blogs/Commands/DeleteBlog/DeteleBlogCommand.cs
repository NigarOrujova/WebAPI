using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;
using System.Linq.Expressions;

namespace Application.Blogs.Commands.DeleteBlog;

public record DeteleBlogCommand(int Id) : IRequest<bool>;

internal class DeteleBlogCommandHandler : IRequestHandler<DeteleBlogCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public DeteleBlogCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<bool> Handle(DeteleBlogCommand request, CancellationToken cancellationToken)
    {
        Blog blog = await _unitOfWork.BlogRepository.GetAsync(n => n.Id == request.Id, includes: new Expression<Func<Blog, object>>[]
        {
            x => x.TagCloud
        })
        ?? throw new NullReferenceException();
        if(blog.ImagePath!=null)
        {
            _env.ArchiveImage(blog.ImagePath);
        }
        await _unitOfWork.BlogRepository.DeleteAsync(blog);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}