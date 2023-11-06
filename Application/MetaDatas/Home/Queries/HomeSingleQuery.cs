using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Home.Queries;

public record HomeSingleQuery : IRequest<Domain.Entities.Home>;
internal class HomeSingleQueryHandler : IRequestHandler<HomeSingleQuery, Domain.Entities.Home>
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Entities.Home> Handle(HomeSingleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.HomeRepository.GetAsync()
            ?? throw new NullReferenceException();

        return entity;
    }
}