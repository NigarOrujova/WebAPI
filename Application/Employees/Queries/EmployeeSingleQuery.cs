using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Employees.Queries;

public record EmployeeSingleQuery : IRequest<EmployeesPage>;

internal class EmployeeSingleQueryHandler : IRequestHandler<EmployeeSingleQuery, EmployeesPage>
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeesPage> Handle(EmployeeSingleQuery request, CancellationToken cancellationToken)
    {
        EmployeesPage entity = await _unitOfWork.EmployeesPageRepository.GetAsync()
            ?? throw new NullReferenceException();

        return entity;
    }
}