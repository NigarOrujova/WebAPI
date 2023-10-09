using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.PortfolioImages.Queries;

public record PortfolioImageSingleQuery(int Id) : IRequest<PortfolioImage>;

internal class PortfolioImageSingleQueryHandler : IRequestHandler<PortfolioImageSingleQuery, PortfolioImage>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioImageSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PortfolioImage> Handle(PortfolioImageSingleQuery request, CancellationToken cancellationToken)
    {
        PortfolioImage entity = await _unitOfWork.PortfolioImageRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}