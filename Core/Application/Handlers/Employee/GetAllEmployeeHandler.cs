using Application.Interfaces.Repositories;
using Application.Interfaces;
using Application.Specifications.Base;
using Domain.ViewModels;
using MediatR;
using Newtonsoft.Json;
using SharedKernel.Claims;
using SharedKernel.Helpers;
using static Application.Specifications.Specs;
namespace Application.Handlers.Employee;

public record GetAllEmployeeHandler(IRepository Repository) : IGetAllQueryHandler<Emp_Dto>
{
    public async Task<Response<IEnumerable<Emp_Dto>?>> Handle(GetAllQueryRequest<Emp_Dto> request, CancellationToken cancellationToken)
    {
        var GetAllResponse = await Repository.GetAllAsync(EmployeeSpecs.GetAllEmployeeSpecs(request.GetAllParams));
        return GetAllResponse.Status == Status.Exception ? GetAllResponse.Exception! : ResponseResult.From(GetAllResponse.Value!);
    }
}
