namespace Application.Handlers.Employee;
using Application.Interfaces.Repositories;
using Application.Interfaces;
using Domain.ViewModels;
using Domain.Models;
using MediatR;
using SharedKernel.Claims;
using SharedKernel.Helpers;

internal record UpdateEmployeeHandler(IRepository Repository, IClaims QClaims) : ICommandHandler<Update_Emp_Dto>
{
    async Task<Response<long?>> IRequestHandler<CommandRequest<Update_Emp_Dto>, Response<long?>>.Handle(CommandRequest<Update_Emp_Dto> request, CancellationToken cancellationToken)
    {
        var hasException = await Repository.AnyAsync(Specifications.Specs.EmployeeSpecs.CheckEmployeeAlreadyExists(request.Dto.OrgId, request.Dto.EmployeeCode!), cancellationToken, false, true);
        if (hasException.Status is Status.Exception)
            return hasException.Exception!;

        var ModelResult = await Repository.FirstOrDefaultAsync(Specifications.Specs.Common.GetByColumn<Employee>("EmployeeCode", request.Dto.EmployeeCode), cancellationToken);
        if (ModelResult.Status is Status.Exception)
            return ModelResult.Exception!;

        ModelResult.Value!.EmployeeName = request.Dto.EmployeeName;
        ModelResult.Value.EmployeeCode = request.Dto.EmployeeCode;
        ModelResult.Value.Active = request.Dto.Active;
        ModelResult.Value.OrgId = request.Dto.OrgId;
        ModelResult.Value.ModifiedDate = DateTime.UtcNow;

        var RepositoryUpdateResult = await Repository.UpdateAsync(ModelResult.Value, cancellationToken, true);

        if (RepositoryUpdateResult.Status == Status.Exception)
            return RepositoryUpdateResult.Exception!;
        return ModelResult.Value.EmployeeCode;
    }
}
