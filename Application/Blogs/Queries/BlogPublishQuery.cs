using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.Blogs.Queries;

public record BlogPublishQuery(int Id) : IRequest<string>;
public class BlogPublishQueryHandler : IRequestHandler<BlogPublishQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogPublishQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<string> Handle(BlogPublishQuery request, CancellationToken cancellationToken)
    {
        var post = await _unitOfWork.BlogRepository.GetAsync(x => x.Id == request.Id);

        if (post == null)
        {
            return "Bu post movcud deyil!";
        }
        else if (post.DeletedAt)
        {
            return "Bu post silindiyi ucun derc edile bilmez!";
        }
        else if (post.PublishDate != null)
        {
            return "Bu post artiq derc edilib!";
        }


        post.PublishDate = DateTime.UtcNow.AddHours(4);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Bu post derc edildi!";
    }
}
