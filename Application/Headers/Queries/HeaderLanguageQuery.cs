using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Headers.Queries;

public record HeaderLanguageQuery(int Id) : IRequest<object>;
internal class HeaderLanguageQueryHandler : IRequestHandler<HeaderLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public HeaderLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(HeaderLanguageQuery request, CancellationToken cancellationToken)
    {
        Header entity = await _unitOfWork.HeaderRepository.GetAsync(
            n => n.Id == request.Id) ?? throw new NullReferenceException();

        var data = new
        {
            Header_en = new
            {
                Title = entity.Title ?? ""
            },
            Header_az = new
            {
                Title = entity.TitleAz
            }
        };
        return data;
    }
}