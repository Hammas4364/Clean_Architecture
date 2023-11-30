namespace Application.Handlers.Employee;
using Application.Interfaces.Repositories;
using Application.Interfaces;
using Domain.ViewModels;
using Domain.Models;
using MediatR;
using SharedKernel.Helpers;
using System.Threading.Tasks;


internal record AddOrUpdateEmployeeHandler(IRepository Repository) : ICommandHandler<Update_Emp_Dto>
{
    async Task<Response<long?>> IRequestHandler<CommandRequest<Update_Emp_Dto>, Response<long?>>.Handle(CommandRequest<Update_Emp_Dto> request, CancellationToken cancellationToken)
    {
        Response<Employee?> res;

        var hasException = await Repository.AnyAsync(Specifications.Specs.EmployeeSpecs.CheckEmployeeAlreadyExists(request.Dto.OrgId, request.Dto.EmployeeCode!), cancellationToken, false, true);
        if (hasException.Status is Status.Exception)
        {
            Domain.Models.Employee emp = new Domain.Models.Employee();
            emp.EmployeeCode = request.Dto.EmployeeCode;
            emp.EmployeeName = request.Dto.EmployeeName;
            emp.OrgId = request.Dto.OrgId;
            emp.CreatedDate = DateTime.UtcNow;
            emp.Active = true;
            res = await Repository.AddAsync(emp, default, true);
        }
        else
        {
            var ModelResult = await Repository.FirstOrDefaultAsync(Specifications.Specs.Common.GetByColumn<Employee>("EmployeeCode", request.Dto.EmployeeCode), cancellationToken);
            if (ModelResult.Status is Status.Exception)
                return ModelResult.Exception!;

            ModelResult.Value!.EmployeeName = request.Dto.EmployeeName;
            ModelResult.Value.EmployeeCode = request.Dto.EmployeeCode;
            ModelResult.Value.Active = request.Dto.Active;
            ModelResult.Value.OrgId = request.Dto.OrgId;
            ModelResult.Value.ModifiedDate = DateTime.UtcNow;

            res = await Repository.UpdateAsync(ModelResult.Value, cancellationToken, true);
        }

        if (res.Status == Status.Exception)
            return res.Exception!;
        return res.Value!.EmployeeCode;
    }
}
