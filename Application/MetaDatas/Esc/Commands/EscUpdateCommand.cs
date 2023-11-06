using Application.Abstracts.Common.Interfaces;
using Application.MetaDatas.Esc.Commands;
using MediatR;

namespace Application.MetaDatas.Esc.Commands;

public class EscUpdateCommand:IRequest<Domain.Entities.Esc>
{
    public string? MetaKeyword { get; set; }
    public string? MetaTitle { get; set; }
    public string? OgTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgDescription { get; set; }
    public string? OgSiteName { get; set; }
    public string? MobileTitle { get; set; }
    public string? AppName { get; set; }
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

        entity.MetaTitle = request.MetaTitle;
        entity.MetaKeyword = request.MetaKeyword;
        entity.OgTitle = request.OgTitle;
        entity.MetaDescription = request.MetaDescription;
        entity.OgDescription = request.OgDescription;
        entity.OgSiteName = request.OgSiteName;
        entity.MobileTitle = request.MobileTitle;
        entity.AppName = request.AppName;

        await _unitOfWork.EscRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity;
    }
}