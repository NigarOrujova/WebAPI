using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.OurValues.Queries;

public record OurValueLanguageQuery(int Id):IRequest<object>;
internal class OurValueLanguageQueryHandler : IRequestHandler<OurValueLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public OurValueLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(OurValueLanguageQuery request, CancellationToken cancellationToken)
    {
        OurValue entity = await _unitOfWork.OurValueRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        var data = new
        {
            ourvalue_en = new
            {
                Title = entity.Title ?? "",
                Description = entity.Description ?? "",
            },
            ourvalue_az = new
            {
                TitleAz = entity.TitleAz ?? "",
                DescriptionAz = entity.DescriptionAz ?? "",
            }
        };
        return data;
    }
}