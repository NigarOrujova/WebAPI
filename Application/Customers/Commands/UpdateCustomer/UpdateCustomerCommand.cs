using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand(int Id,Customer Customer) : IRequest<Customer>;
public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer entity = await _unitOfWork.CustomerRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();


        if (request.Customer.Image == null)
        {
            request.Customer.Image = entity.Image;
            goto save;
        }

        if (!request.Customer.Image.CheckFileSize(1000))
            throw new FileException("File max size 1 mb");
        if (!request.Customer.Image.CheckFileType("image/"))
            throw new FileException("File type must be image");
        string newImageName = request.Customer.Image.GetRandomImagePath("customer");

        _env.ArchiveImage(entity.ImagePath);
        await _env.SaveAsync(request.Customer.Image, newImageName, cancellationToken);

        entity.ImagePath = newImageName;

    save:
        entity.ImageAlt = request.Customer.ImageAlt;

        await _unitOfWork.CustomerRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}