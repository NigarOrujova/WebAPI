using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Employees.Queries;

public record EmployeeLanguageQuery : IRequest<object>;

internal class EmployeeLanguageQueryHandler : IRequestHandler<EmployeeLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(EmployeeLanguageQuery request, CancellationToken cancellationToken)
    {
        EmployeesPage entity = await _unitOfWork.EmployeesPageRepository.GetAsync()
            ?? throw new NullReferenceException();
        var data = new
        {
            employee_en = new
            {
                entity.Title,
                entity.SubTitle,
                entity.Description,
                entity.Title2,
                entity.SubTitle2,
                entity.Description2,
                entity.Title3,
                entity.FullName,
                entity.ImagePath,
                entity.FullName2,
                entity.ImagePath2,
                entity.ImageAlt,
                entity.ImageAlt2
            },
            employee_az = new
            {
                Title = entity.TitleAz,
                SubTitle = entity.SubTitleAz,
                Description = entity.DescriptionAz,
                Title2 = entity.TitleAz2,
                SubTitle2 = entity.SubTitleAz2,
                Description2 = entity.DescriptionAz2,
                Title3 = entity.TitleAz3,
                FullName = entity.FullNameAz,
                ImagePath = entity.ImagePath,
                FullName2 = entity.FullNameAz2,
                ImagePath2 = entity.ImagePath2,
                ImageAlt = entity.ImageAltAz,
                ImageAlt2 = entity.ImageAltAz2
            }
        };
        return data;
    }
}
