namespace Application.Handlers;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using MediatR;
using Domain.ViewModels;
using Domain.Models;
using SharedKernel.Helpers;
using System.Threading.Tasks;

internal record AddEmployeeHandler(IRepository Repository) : ICommandHandler<AddEmployee>
{
    async Task<Response<long?>> IRequestHandler<CommandRequest<AddEmployee>, Response<long?>>.Handle(CommandRequest<AddEmployee> request, CancellationToken cancellationToken)
    {
        Employee emp = new Employee();
        emp.EmployeeCode = request.Dto.EmployeeCode;
        emp.EmployeeName = request.Dto.EmployeeName;
        emp.Active = true;

        var RepositoryAddResult = await Repository.AddAsync(emp,default,true);

        if (RepositoryAddResult.Status == Status.Exception)
            return RepositoryAddResult.Exception!;
        return RepositoryAddResult.Value!.Id;
    }
}
