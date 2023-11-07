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
            employee_aze = new
            {
                Title = entity.Title,
                SubTitle = entity.SubTitle,
                Description = entity.Description,
                Title2 = entity.Title2,
                SubTitle2 = entity.SubTitle2,
                Description2 = entity.Description2,
                Title3 = entity.Title3,
                FullName = entity.FullName,
                ImagePath = entity.ImagePath,
                FullName2 = entity.FullName2,
                ImagePath2 = entity.ImagePath2,
                ImageAlt = entity.ImageAlt,
                ImageAlt2 = entity.ImageAlt2
            },
            employee_eng = new
            {
                Title = entity.Title,
                SubTitle = entity.SubTitle,
                Description = entity.Description,
                Title2 = entity.Title2,
                SubTitle2 = entity.SubTitle2,
                Description2 = entity.Description2,
                Title3 = entity.Title3,
                FullName = entity.FullName,
                ImagePath = entity.ImagePath,
                FullName2 = entity.FullName2,
                ImagePath2 = entity.ImagePath2,
                ImageAlt = entity.ImageAlt,
                ImageAlt2 = entity.ImageAlt2
            }
        };
        return data;
    }
}
