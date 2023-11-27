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

internal record AddOrganizationHandler(IRepository Repository) : ICommandHandler<Add_Org_Dto>
{
    async Task<Response<long?>> IRequestHandler<CommandRequest<Add_Org_Dto>, Response<long?>>.Handle(CommandRequest<Add_Org_Dto> request, CancellationToken cancellationToken)
    {
        var orgExistResult = await Repository.AnyAsync(
            Specs.Common.GetByColumn<Organization>(CommonFields.Name, request.Dto.OrgName),
             cancellationToken, false, true);

        if (orgExistResult.Status is Status.Exception)
            return orgExistResult.Exception!;

        var org = Organization.Create(request.Dto);
        Serilog.Log.Verbose(Repository.DbContext.ChangeTracker.DebugView.LongView);

        var RepositoryAddResult = await Repository.AddAsync(org);

        if (RepositoryAddResult.Status == Status.Exception)
            return RepositoryAddResult.Exception!;
        return RepositoryAddResult.Value!.Id;
    }
}
