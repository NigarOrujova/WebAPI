using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Headers.Queries;

public record HeaderLanguageAllQuery : IRequest<object>;
public class HeaderLanguageAllQueryHandler : IRequestHandler<HeaderLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public HeaderLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(HeaderLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Header> Headers = await _unitOfWork.HeaderRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        var data = new
        {
            Header_en = Headers.Select(p => new
            {
                p.Id,
                Title = p.Title ?? ""
            }),
            Header_az = Headers.Select(p => new
            {
                p.Id,
                Title = p.TitleAz ?? ""
            })
        };

        return data;
    }
}