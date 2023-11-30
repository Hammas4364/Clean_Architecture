using Application.Interfaces;
using Application.Interfaces.Logics;
using Application.Interfaces.Repositories;
using Application.Specifications.Base;
using Domain.ViewModels;
using SharedKernel.Helpers;

namespace Application.Handlers.Employee;

internal record GetEmployeeByOrgHandler(IRepository Repository) : IGetAllQueryHandler<Get_EmpByOrgId_Dto>
{
    public async Task<Response<IEnumerable<Get_EmpByOrgId_Dto>?>> Handle(GetAllQueryRequest<Get_EmpByOrgId_Dto> request, CancellationToken cancellationToken)
    {
        var specs = new GenericASpec<Domain.Models.Employee, Get_EmpByOrgId_Dto>()
        {
            SpecificationFunc = _ =>
             (request.GetAllParams.SearchValue is not null ? _.Where(_ => _.OrgId.ToString().ToLower().Contains(request.GetAllParams.SearchValue.ToLower())) : _)
             .Select(_ => new Get_EmpByOrgId_Dto
             {
                 Id = _.Id
             }).Pagging(request.GetAllParams.PageIndex, request.GetAllParams.PageSize)
        };
        var result = await Repository.GetAllAsync(specs, cancellationToken);
        return result;
    }
}