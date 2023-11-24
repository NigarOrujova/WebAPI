using Application.Abstracts.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Blogs.Commands.CreateBlog;

public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public CreateBlogCommandValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        RuleFor(blog => blog.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MustAsync(BeUniqueSlugAsync).WithMessage("Title must be unique.");
    }

    private async Task<bool> BeUniqueSlugAsync(string slug, CancellationToken cancellationToken)
    {
        var blogRepository = _serviceProvider.GetRequiredService<IBlogRepository>();
        return !await blogRepository.IsExistAsync(b => b.Slug == slug);
    }
}