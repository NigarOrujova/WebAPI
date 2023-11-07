using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Queries;

public record TeamLanguageQuery(int Id):IRequest<object>;
internal class TeamLanguageQueryHandler : IRequestHandler<TeamLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public TeamLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(TeamLanguageQuery request, CancellationToken cancellationToken)
    {
        Team entity = await _unitOfWork.TeamRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}
