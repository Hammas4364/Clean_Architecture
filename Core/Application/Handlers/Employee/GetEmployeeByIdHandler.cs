namespace Application.Handlers.Employee;
using Application.Interfaces.Repositories;
using Application.Interfaces;
using Application.Specifications.Base;
using Domain.ViewModels;
using Domain.Models;
using SharedKernel.Claims;
using SharedKernel.Helpers;


internal record GetEmployeeByIdHandler(IRepository Repository, IClaims QClaims) : IQueryHandler<long, Emp_Dto>
{
    public async Task<Response<Emp_Dto?>> Handle(QueryRequest<long, Emp_Dto> request, CancellationToken cancellationToken)
    {       

        var specs = new GenericASpec<Employee, Emp_Dto>()
        {
            SpecificationFunc = _ => _.Where(_ => _.EmployeeCode == request.Request)
             .Select(_ => new Emp_Dto()
             {
                 Id = _.Id,
                 OrgId = _.OrgId!,
                 EmployeeCode = _.EmployeeCode!,
                 EmployeeName = _.EmployeeName!,
                 Active = _.Active,
                 CreatedDate = _.CreatedDate!,
                 ModifiedDate = _.ModifiedDate!,
             })
        };
        var repoResult = await Repository.FirstOrDefaultAsync(specs, cancellationToken, true, false);
        if (repoResult.Status is Status.Exception)
            return repoResult.Exception!;
        return repoResult.Value;
    }
}