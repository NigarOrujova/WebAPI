using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.We.Commands;

public record WeUpdateCommand : IRequest<Domain.Entities.We>
{
    public string? MetaTitle { get; set; }
    public string? OgTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgDescription { get; set; }
    public string? OgSiteName { get; set; }
    public string? MobileTitle { get; set; }
    public string? AppName { get; set; }
}
public class WeUpdateCommandHandler : IRequestHandler<WeUpdateCommand, Domain.Entities.We>
{
    private readonly IUnitOfWork _unitOfWork;

    public WeUpdateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Domain.Entities.We> Handle(WeUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.WeRepository.GetAsync()
        ?? throw new NotImplementedException();

        entity.MetaTitle = request.MetaTitle;
        entity.OgTitle = request.OgTitle;
        entity.MetaDescription = request.MetaDescription;
        entity.OgDescription = request.OgDescription;
        entity.OgSiteName = request.OgSiteName;
        entity.MobileTitle = request.MobileTitle;
        entity.AppName = request.AppName;

        await _unitOfWork.WeRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity;
    }
}