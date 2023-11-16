using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Footers.Commands.UpdateFooter;

public record UpdateFooterCommand : IRequest<Footer>
{
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? AddressAz { get; set; }
}
public class UpdateFooterCommandHandler : IRequestHandler<UpdateFooterCommand, Footer>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFooterCommandHandler(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }

    public async Task<Footer> Handle(UpdateFooterCommand request, CancellationToken cancellationToken)
    {
        Footer entity = await _unitOfWork.FooterRepository.GetAsync()
             ?? throw new NullReferenceException();

        entity.Phone = request.Phone;
        entity.Email = request.Email;
        entity.Address = request.Address;
        entity.AddressAz = request.AddressAz;

        await _unitOfWork.FooterRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}