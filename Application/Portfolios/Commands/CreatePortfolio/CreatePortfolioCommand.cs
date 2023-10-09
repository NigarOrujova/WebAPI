using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Portfolios.Commands.CreatePortfolio;

public record CreatePortfolioCommand : IRequest<int>
{
    public string? Title { get; init; }
    public string? SubTitle { get; init; }
    public string? Description { get; init; }
    public bool IsMain { get; init; }
}
public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePortfolioCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var entity = new Portfolio();

        entity.Title = request.Title;
        entity.SubTitle = request.SubTitle;
        entity.Description = request.Description;
        entity.IsMain = request.IsMain;

        await _unitOfWork.PortfolioRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}