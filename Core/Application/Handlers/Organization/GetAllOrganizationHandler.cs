namespace Application.Handlers.Organization;
using Application.Interfaces.Repositories;
using Application.Interfaces;
using Application.Specifications.Base;
using MediatR;
using Newtonsoft.Json;
using SharedKernel.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ViewModels;
using SharedKernel.Claims;

public record GetAllOrganizationHandler(IDapperRepository Repository, IClaims QClaims) : IGetAllQueryHandler<Get_Ord_Dto>
{
    async Task<Response<IEnumerable<Get_Ord_Dto>?>> IRequestHandler<GetAllQueryRequest<Get_Ord_Dto>, Response<IEnumerable<Get_Ord_Dto>?>>.Handle(GetAllQueryRequest<Get_Ord_Dto> request, CancellationToken cancellationToken)
    {
        var getall_Scroll_Specs = new GenericDSpec<string>
        {
            CommandText = @"SP_GetAll_Organinzation",
            Parameters = new { SearchValue = request.GetAllParams.SearchValue, PageNumber = request.GetAllParams.PageIndex ?? 1, PageSize = request.GetAllParams.PageSize ?? 50 }
        };
        var dbJsonResult = await Repository.ExecuteScalarAsync(getall_Scroll_Specs, false, cancellationToken);
        if (dbJsonResult.Status is Status.Exception)
            return dbJsonResult.Exception!;
        if (dbJsonResult.Status is Status.NotFound)
            return ResponseResult.OK<IEnumerable<Get_Ord_Dto>?>(Enumerable.Empty<Get_Ord_Dto>());

        var result = JsonConvert.DeserializeObject<IEnumerable<Get_Ord_Dto>?>(dbJsonResult.Value!);
        return ResponseResult.From(result);
    }
}
