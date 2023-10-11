using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Footers.Queries;

public record FooterSingleQuery: IRequest<Footer>;

internal class FooterSingleQueryHandler : IRequestHandler<FooterSingleQuery, Footer>
{
    private readonly IUnitOfWork _unitOfWork;

    public FooterSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Footer> Handle(FooterSingleQuery request, CancellationToken cancellationToken)
    {
        Footer entity = await _unitOfWork.FooterRepository.GetAsync()
            ?? throw new NullReferenceException();

        return entity;
    }
}