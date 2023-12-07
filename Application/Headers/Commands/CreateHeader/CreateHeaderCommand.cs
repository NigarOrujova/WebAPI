using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Headers.Commands.CreateHeader;

public record CreateHeaderCommand : IRequest<int>
{
    public string Title { get; set; }
    public string TitleAz { get; set; }
}
public class CreateHeaderCommandHandler : IRequestHandler<CreateHeaderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateHeaderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateHeaderCommand request, CancellationToken cancellationToken)
    {
        var entity = new Header();

        entity.Title = request.Title;
        entity.TitleAz = request.TitleAz;
        await _unitOfWork.HeaderRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}