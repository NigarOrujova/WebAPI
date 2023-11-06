using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Contact.Queries;

public record ContactSingleQuery:IRequest<Domain.Entities.Contact>;

internal class ContactSingleQueryHandler : IRequestHandler<ContactSingleQuery, Domain.Entities.Contact>
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Entities.Contact> Handle(ContactSingleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ContactRepository.GetAsync()
            ?? throw new NullReferenceException();

        return entity;
    }
}
