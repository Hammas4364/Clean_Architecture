namespace Application.Handlers.Organization;
using Application.Interfaces.Repositories;
using Application.Interfaces;
using MediatR;
using SharedKernel.Helpers;
using System.Threading.Tasks;
using SharedKernel.Claims;
using Domain.ViewModels;
using Domain.Models;
using Specifications;

internal record DeleteOrganizationHandler(IClaims Claims, IRepository Repository) : ICommandHandler<Delete_Org_Dto>
{
    async Task<Response<long?>> IRequestHandler<CommandRequest<Delete_Org_Dto>, Response<long?>>.Handle(CommandRequest<Delete_Org_Dto> request, CancellationToken cancellationToken)
    {
        var OrgModelResult = await Repository.FirstOrDefaultAsync(Specifications.Specs.Common.GetById<Organization, long>(request.Dto.Id), cancellationToken);
        if (OrgModelResult.Status is Status.Exception)
            return OrgModelResult.Exception!;

        var RepositoryDeleteResult = await Repository.DeleteAsync(OrgModelResult.Value!, cancellationToken, true);

        if (RepositoryDeleteResult.Status == Status.Exception)
            return RepositoryDeleteResult.Exception!;
        return OrgModelResult.Value!.Id;
    }
}
