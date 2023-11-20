using Application.Abstracts.Common.Interfaces;
using Application.MetaDatas.Esc.Commands;
using MediatR;

namespace Application.MetaDatas.Esc.Commands;

public class EscUpdateCommand:IRequest<Domain.Entities.Esc>
{
    public string? MetaKeyword { get; set; }
    public string? MetaKeywordAz { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaTitleAz { get; set; }
    public string? OgTitle { get; set; }
    public string? OgTitleAz { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaDescriptionAz { get; set; }
    public string? OgDescription { get; set; }
    public string? OgDescriptionAz { get; set; }
    public string? OgSiteName { get; set; }
    public string? OgSiteNameAz { get; set; }
    public string? MobileTitle { get; set; }
    public string? MobileTitleAz { get; set; }
    public string? AppName { get; set; }
    public string? AppNameAz { get; set; }
}
public class EscUpdateCommandHandler : IRequestHandler<EscUpdateCommand, Domain.Entities.Esc>
{
    private readonly IUnitOfWork _unitOfWork;

    public EscUpdateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Domain.Entities.Esc> Handle(EscUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.EscRepository.GetAsync()
        ?? throw new NotImplementedException();

        entity.MetaKeyword = request.MetaKeyword;
        entity.MetaKeywordAz = request.MetaKeywordAz;
        entity.MetaTitle = request.MetaTitle;
        entity.MetaTitleAz = request.MetaTitleAz;
        entity.OgTitle = request.OgTitle;
        entity.OgTitleAz = request.OgTitleAz;
        entity.MetaDescription = request.MetaDescription;
        entity.MetaDescriptionAz = request.MetaDescriptionAz;
        entity.OgDescription = request.OgDescription;
        entity.OgDescriptionAz = request.OgDescriptionAz;
        entity.OgSiteName = request.OgSiteName;
        entity.OgSiteNameAz = request.OgSiteNameAz;
        entity.MobileTitle = request.MobileTitle;
        entity.MobileTitleAz = request.MobileTitleAz;
        entity.AppName = request.AppName;
        entity.AppNameAz = request.AppNameAz;

        await _unitOfWork.EscRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity;
    }
}