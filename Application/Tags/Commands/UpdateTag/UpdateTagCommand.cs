using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Tags.Commands.UpdateTag;

public record UpdateTagCommand(int Id, Tag Tag) : IRequest<Tag>;
public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Tag>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public UpdateTagCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<Tag> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        Tag entity = await _unitOfWork.TagRepository.GetAsync(n => n.Id == request.Id);
       
        entity.Name = request.Tag.Name;
        entity.NameAz = request.Tag.NameAz;

        //if (request.Tag.TagIds != null)
        //{
        //    entity.TagCloud?.RemoveAll(x => !request.Tag.TagIds.Contains(x.TagId));
        //    foreach (var categoryId in request.Tag.TagIds.Where(x => !entity.TagCloud.Any(rc => rc.TagId == x)))
        //    {
        //        TagTagCloud TagCategory = new TagTagCloud
        //        {
        //            TagId = request.Id,
        //            TagId = categoryId
        //        };
        //        entity.TagCloud.Add(TagCategory);
        //    }
        //}
        await _unitOfWork.TagRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}