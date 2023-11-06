﻿using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Love.Commands;

public record LoveUpdateCommand : IRequest<Domain.Entities.Love>
{
    public string? MetaTitle { get; set; }
    public string? OgTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgDescription { get; set; }
    public string? OgSiteName { get; set; }
    public string? MobileTitle { get; set; }
    public string? AppName { get; set; }
}
public class LoveUpdateCommandHandler : IRequestHandler<LoveUpdateCommand, Domain.Entities.Love>
{
    private readonly IUnitOfWork _unitOfWork;

    public LoveUpdateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Domain.Entities.Love> Handle(LoveUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.LoveRepository.GetAsync()
        ?? throw new NotImplementedException();

        entity.MetaTitle = request.MetaTitle;
        entity.OgTitle = request.OgTitle;
        entity.MetaDescription = request.MetaDescription;
        entity.OgDescription = request.OgDescription;
        entity.OgSiteName = request.OgSiteName;
        entity.MobileTitle = request.MobileTitle;
        entity.AppName = request.AppName;

        await _unitOfWork.LoveRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity;
    }
}