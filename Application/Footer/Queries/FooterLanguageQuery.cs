using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.Footers.Queries;

public record FooterLanguageQuery : IRequest<object>;

internal class FooterLanguageQueryHandler : IRequestHandler<FooterLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public FooterLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(FooterLanguageQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.FooterRepository.GetAsync(
        includes: x => x.Medias)
            ?? throw new NullReferenceException();

        var data = new
        {
            footer_en = new
            {
                entity.Phone,
                entity.Email,
                entity.Address,
                Medias= entity.Medias.Select(m => new
                {
                    m.Title,
                    m.URL
                })
            },
            footer_az=new 
            {
                entity.Phone,
                entity.Email,
                Address = entity.AddressAz,
                Medias = entity.Medias.Select(m => new
                {
                    m.Title,
                    m.URL
                })
            }
        };

        return data;
    }
}