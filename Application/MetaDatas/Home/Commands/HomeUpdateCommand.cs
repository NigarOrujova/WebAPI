using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Home.Commands;

public record HomeUpdateCommand:IRequest<Domain.Entities.Home>
{
    public string? MetaTitle { get; set; }
    public string? OgTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgDescription { get; set; }
    public string? OgSiteName { get; set; }
    public string? MobileTitle { get; set; }
    public string? AppName { get; set; }
}
public class HomeUpdateCommandHandler : IRequestHandler<HomeUpdateCommand, Domain.Entities.Home>
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeUpdateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Domain.Entities.Home> Handle(HomeUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.HomeRepository.GetAsync()
        ?? throw new NotImplementedException();

        entity.MetaTitle = request.MetaTitle;
        entity.OgTitle = request.OgTitle;
        entity.MetaDescription = request.MetaDescription;
        entity.OgDescription = request.OgDescription;
        entity.OgSiteName = request.OgSiteName;
        entity.MobileTitle = request.MobileTitle;
        entity.AppName = request.AppName;

        await _unitOfWork.HomeRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity;
    }
}