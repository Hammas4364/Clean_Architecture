namespace Application.Handlers.Organization;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Specifications;
using Domain.ViewModels;
using Domain.Models;
using MediatR;
using SharedKernel.Helpers;
using System.Threading.Tasks;
using Application.Specifications.Base;
using Newtonsoft.Json;
using SharedKernel.Exceptions;
using SharedKernel.Claims;

internal record GetOrganizationByIdHandler(IRepository Repository, IClaims QClaims) : IQueryHandler<long, Get_OrgById_Dto>
{
    public async Task<Response<Get_OrgById_Dto?>> Handle(QueryRequest<long, Get_OrgById_Dto> request, CancellationToken cancellationToken)
    {
        var getAllSpec = new GenericASpec<Organization, Get_OrgById_Dto>()
        {
            SpecificationFunc = _ => _.Where(request.Request)
            .Select(_ => new Get_OrgById_Dto()
            {
                Id = _.Id,
                OrgName = _.OrgName!,
                OrgDetail = _.OrgDetail!,
                Token = _.Token!,
                Active = _.Active,
                CreatedDate = _.CreatedDate.ToString()!,
                ModifiedDate = _.ModifiedDate.ToString()!,
            })
        };
        var getAllControllerResult = await Repository.FirstOrDefaultAsync(getAllSpec, cancellationToken);
        if (getAllControllerResult.Status is Status.Exception)
            return getAllControllerResult.Exception!;

        return getAllControllerResult;
    }
}


