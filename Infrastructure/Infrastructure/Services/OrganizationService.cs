namespace Infrastructure.Services;
using Application.Common.Sender;
using Application.Interfaces.Services;
using AutoWrapper.Wrappers;
using Domain.ViewModels;
using SharedKernel.Helpers;
using System.Security.Claims;
using System.Threading.Tasks;


public record OrganizationService(IASender Sender) : IOrganizationService
{
    public async Task<ApiResponse> AddAsync(Add_Org_Dto dto, CancellationToken cancellationToken = new())
    {     
        var OrgIdResult = await Sender.Send(new CommandRequest<Add_Org_Dto>(dto), cancellationToken);
        if (OrgIdResult.Status is Status.Exception)
            throw OrgIdResult.Exception!;
        return OrgIdResult.Result!;
    }

    public async Task<ApiResponse> UpdateAsync(Update_Org_Dto dto, CancellationToken cancellationToken = new())
    {
        var controllerIdResult = await Sender.Send(new CommandRequest<Update_Org_Dto>(dto), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }

    public async Task<ApiResponse> DeleteAsync(Delete_Org_Dto dto, CancellationToken cancellationToken = new())
    {
        var controllerIdResult = await Sender.Send(new CommandRequest<Delete_Org_Dto>(dto), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }

    async Task<ApiResponse> IOrganizationService.GetAllAsync(GetAllParams getAllParams, CancellationToken cancellationToken)
    {
        var controllerIdResult = await Sender.Send(new GetAllQueryRequest<Get_Org_Dto>(getAllParams), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }

    async Task<ApiResponse> IOrganizationService.GetAsync(long id, CancellationToken cancellationToken)
    {
        var controllerIdResult = await Sender.Send(new QueryRequest<long, Get_OrgById_Dto>(id), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }
}

