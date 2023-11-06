using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Contact.Commands;

public record ContactUpdateCommand :IRequest<Domain.Entities.Contact>
{
    public string? MetaTitle { get; set; }
    public string? OgTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgDescription { get; set; }
    public string? OgSiteName { get; set; }
    public string? MobileTitle { get; set; }
    public string? AppName { get; set; }
}
public class ContactUpdateCommandHandler : IRequestHandler<ContactUpdateCommand, Domain.Entities.Contact>
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactUpdateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Domain.Entities.Contact> Handle(ContactUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ContactRepository.GetAsync()
        ??  throw new NotImplementedException();

        entity.MetaTitle= request.MetaTitle;
        entity.OgTitle= request.OgTitle;
        entity.MetaDescription= request.MetaDescription;
        entity.OgDescription= request.OgDescription;
        entity.OgSiteName= request.OgSiteName;
        entity.MobileTitle= request.MobileTitle;
        entity.AppName= request.AppName;

        await _unitOfWork.ContactRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
