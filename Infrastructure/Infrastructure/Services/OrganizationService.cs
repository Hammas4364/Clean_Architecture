namespace Infrastructure.Services;
using Application.Common.Sender;
using Application.Interfaces.Services;
using AutoWrapper.Wrappers;
using Domain.Behaviours.Common;
using Domain.ViewModels;
using SharedKernel.Helpers;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


public record OrganizationService(IASender Sender) : IOrganizationService
{
    public async Task<ApiResponse> AddAsync(Add_Org_Dto dto, CancellationToken cancellationToken = new())
    {
        //var claims = new List<Claim>
        //{
        //    TokenServices.CreateClaim("Orgnization", dto.OrgName + " (" + dto.OrgDetail + ") - Active"),
        //    TokenServices.CreateClaim("OrgName", dto.OrgName),
        //    TokenServices.CreateClaim("OrgDetail", dto.OrgDetail),
        //    TokenServices.CreateClaim("OrgStatus", dto.Active.ToString())
        //};
        //string token = TokenServices.GenerateToken(claims, "This is my Organization");

        var AddEntity = new Add_Org_Dto(dto.OrgName, dto.OrgDetail, dto.Active, dto.Token);

        var OrgIdResult = await Sender.Send(new CommandRequest<Add_Org_Dto>(AddEntity), cancellationToken);
        if (OrgIdResult.Status is Status.Exception)
            throw OrgIdResult.Exception!;

        return OrgIdResult.Result!;
    }

    public async Task<ApiResponse> UpdateAsync(Update_Org_Dto dto, CancellationToken cancellationToken = new())
    {
        var updateEntity = new Update_Org_Dto(dto.Id, dto.OrgName, dto.OrgDetail, dto.Active);

        var controllerIdResult = await Sender.Send(new CommandRequest<Update_Org_Dto>(updateEntity), cancellationToken);
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
        var controllerIdResult = await Sender.Send(new GetAllQueryRequest<Get_Ord_Dto>(getAllParams), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }

    async Task<ApiResponse> IOrganizationService.GetAsync(long id, CancellationToken cancellationToken)
    {
        var controllerIdResult = await Sender.Send(new QueryRequest<long, Get_OrdById_Dto>(id), cancellationToken);
        if (controllerIdResult.Status is Status.Exception)
            throw controllerIdResult.Exception!;
        return controllerIdResult.Result!;
    }
}

