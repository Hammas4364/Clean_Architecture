using Application.Interfaces.Repositories;
using Application.Interfaces;
using Domain.ViewModels;
using MediatR;
using SharedKernel.Claims;
using SharedKernel.Helpers;

namespace Application.Handlers.Employee;

internal record DeleteEmployeeHandler(IClaims Claims, IRepository Repository) : ICommandHandler<Delete_Emp_Dto>
{
    async Task<Response<long?>> IRequestHandler<CommandRequest<Delete_Emp_Dto>, Response<long?>>.Handle(CommandRequest<Delete_Emp_Dto> request, CancellationToken cancellationToken)
    {
        var ModelResult = await Repository.FirstOrDefaultAsync(Specifications.Specs.Common.GetByColumn<Domain.Models.Employee>("EmployeeCode", request.Dto.EmpCode), cancellationToken);
        if (ModelResult.Status is Status.Exception)
            return ModelResult.Exception!;

        var RepositoryDeleteResult = await Repository.DeleteAsync(ModelResult.Value!, cancellationToken, true);

        if (RepositoryDeleteResult.Status == Status.Exception)
            return RepositoryDeleteResult.Exception!;
        return ModelResult.Value!.EmployeeCode;
    }
}
