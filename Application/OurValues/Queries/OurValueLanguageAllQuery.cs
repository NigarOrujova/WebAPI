using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.OurValues.Queries;

public record OurValueLanguageAllQuery : IRequest<object>;
public class OurValueLanguageAllQueryHandler : IRequestHandler<OurValueLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public OurValueLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(OurValueLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<OurValue> ourValues = await _unitOfWork.OurValueRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        var data = new
        {
            ourvalue_en = ourValues.Select(o => new
            {
                o.Id,
                Title = o.Title ?? "",
                Description = o.Description ?? "",
            }).ToList(),
            ourvalue_az = ourValues.Select(o => new
            {
                o.Id,
                Title = o.TitleAz ?? "",
                Description = o.DescriptionAz ?? "",
            }).ToList()
        };

        return data;
    }
}