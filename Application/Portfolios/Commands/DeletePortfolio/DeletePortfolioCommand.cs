using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Portfolios.Commands.DeletePortfolio;

public record DeletePortfolioCommand(int Id) : IRequest<bool>;

internal class DeletePortfolioCommandHandler : IRequestHandler<DeletePortfolioCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePortfolioCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
    {
        Portfolio Portfolio = await _unitOfWork.PortfolioRepository.GetAsync(n => n.Id == request.Id, includes: new Expression<Func<Portfolio, object>>[]
            {
                x => x.PortfolioCategories,
                x => x.Images
            })
            ?? throw new NullReferenceException();

        await _unitOfWork.PortfolioRepository.DeleteAsync(Portfolio);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
