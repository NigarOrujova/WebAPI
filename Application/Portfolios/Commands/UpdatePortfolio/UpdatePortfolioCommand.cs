using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Portfolios.Commands.UpdatePortfolio;

public record UpdatePortfolioCommand : IRequest<Portfolio>
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? SubTitle { get; init; }
    public string? Description { get; init; }
    public bool IsMain { get; init; }
}
public class UpdatePortfolioCommandHandler : IRequestHandler<UpdatePortfolioCommand, Portfolio>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePortfolioCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Portfolio> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
    {
        Portfolio entity = await _unitOfWork.PortfolioRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.Title = request.Title;
        entity.SubTitle = request.SubTitle;
        entity.Description = request.Description;
        entity.IsMain = request.IsMain;

        await _unitOfWork.PortfolioRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}