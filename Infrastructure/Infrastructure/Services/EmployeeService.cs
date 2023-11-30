namespace Infrastructure.Services;
using Application.Common.Sender;
using Application.Interfaces.Services;
using AutoWrapper.Wrappers;
using Domain.ViewModels;
using MediatR;
using SharedKernel.Helpers;

public record EmployeeService(IASender Sender) : IEmployeeService
{
    public async Task<ApiResponse> AddAsync(Add_Emp_Dto dto, CancellationToken cancellationToken = new())
    {
        var OrgIdResult = await Sender.Send(new CommandRequest<Add_Emp_Dto>(dto), cancellationToken);
        if (OrgIdResult.Status is Status.Exception)
            throw OrgIdResult.Exception!;
        return OrgIdResult.Result!;
    }

    public async Task<ApiResponse> UpdateAsync(Update_Emp_Dto dto, CancellationToken cancellationToken = new())
    {
        var controllerIdResult = await Sender.Send(new CommandRequest<Update_Emp_Dto>(dto), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }

    public async Task<ApiResponse> AddOrUpdateAsync(Update_Emp_Dto dto, CancellationToken cancellationToken = new())
    {
        var controllerIdResult = await Sender.Send(new CommandRequest<Update_Emp_Dto>(dto), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }

    public async Task<ApiResponse> DeleteAsync(Delete_Emp_Dto dto, CancellationToken cancellationToken = new())
    {
        var controllerIdResult = await Sender.Send(new CommandRequest<Delete_Emp_Dto>(dto), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }

    async Task<ApiResponse> IEmployeeService.GetAllAsync(GetAllParams getAllParams, CancellationToken cancellationToken)
    {
        var controllerIdResult = await Sender.Send(new GetAllQueryRequest<Emp_Dto>(getAllParams), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }

    public async Task<ApiResponse> GetEmployeeByOrgAsync(GetAllParams @params, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetAllQueryRequest<Get_EmpByOrgId_Dto>(@params), cancellationToken);
        if (result.Status is Status.Exception)
            throw result.Exception!;
        return result.Result!;
    }

    async Task<ApiResponse> IEmployeeService.GetAsync(long id, CancellationToken cancellationToken)
    {
        var controllerIdResult = await Sender.Send(new QueryRequest<long, Emp_Dto>(id), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }
   
}
