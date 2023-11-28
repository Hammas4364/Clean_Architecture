namespace Application.Handlers.Organization;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Specifications;
using MediatR;
using Domain.ViewModels;
using Domain.Models;
using SharedKernel.Helpers;
using System.Threading.Tasks;
using SharedKernel.Constants;
using System.Security.Claims;
using Application.Common;
using Application.Specifications.Base;

internal record AddOrganizationHandler(IRepository Repository) : ICommandHandler<Add_Org_Dto>
{
    async Task<Response<long?>> IRequestHandler<CommandRequest<Add_Org_Dto>, Response<long?>>.Handle(CommandRequest<Add_Org_Dto> request, CancellationToken cancellationToken)
    {
        Organization org = new Organization();
        org.OrgName = request.Dto.OrgName;
        org.OrgDetail = request.Dto.OrgDetail;
        org.Active = true;
        org.CreatedDate = DateTime.UtcNow;
        org.ModifiedDate = DateTime.UtcNow;
        //var claims = new List<Claim>
        //{
        //    TokenServices.CreateClaim("Orgnization", org.OrgName + " (" + org.OrgDetail + ") - Active"),
        //    TokenServices.CreateClaim("OrgName", org.OrgName!),
        //    TokenServices.CreateClaim("OrgDetail", org.OrgDetail ?? ""),
        //    TokenServices.CreateClaim("OrgStatus", org.Active.ToString()),
        //    TokenServices.CreateClaim("CreateAt", org.CreatedDate.ToString()!)
        //};
        //string token = TokenServices.GenerateToken(claims, "This is my Organization");
        org.Token = "";

        var orgExistResult = await Repository.AnyAsync<Organization>(Specs.Common.GetByColumn<Organization>("OrgName", org.OrgName!), cancellationToken, false, true);
        if (orgExistResult.Status is Status.Exception)
            return orgExistResult.Exception!;

        var RepositoryAddResult = await Repository.AddAsync(org, default, true);

        if (RepositoryAddResult.Status == Status.Exception)
            return RepositoryAddResult.Exception!;
        return RepositoryAddResult.Value!.Id;
    }
}
