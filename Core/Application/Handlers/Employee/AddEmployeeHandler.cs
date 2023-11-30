namespace Application.Handlers;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using MediatR;
using Domain.ViewModels;
using SharedKernel.Helpers;
using System.Threading.Tasks;

internal record AddEmployeeHandler(IRepository Repository) : ICommandHandler<Add_Emp_Dto>
{
    async Task<Response<long?>> IRequestHandler<CommandRequest<Add_Emp_Dto>, Response<long?>>.Handle(CommandRequest<Add_Emp_Dto> request, CancellationToken cancellationToken)
    {
        Domain.Models.Employee emp = new Domain.Models.Employee();
        emp.EmployeeCode = request.Dto.EmployeeCode;
        emp.EmployeeName = request.Dto.EmployeeName;
        emp.OrgId = request.Dto.OrgId;
        emp.CreatedDate = DateTime.UtcNow;
        emp.Active = true;

        var RepositoryAddResult = await Repository.AddAsync(emp,default,true);

        if (RepositoryAddResult.Status == Status.Exception)
            return RepositoryAddResult.Exception!;
        return RepositoryAddResult.Value!.Id;
    }
}
