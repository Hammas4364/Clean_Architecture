namespace Application.Handlers.Organization;
using Application.Interfaces.Repositories;
using Application.Interfaces;
using MediatR;
using SharedKernel.Helpers;
using Domain.ViewModels;
using SharedKernel.Claims;
using Domain.Models;

internal record UpdateOrganizationHandler(IRepository Repository, IClaims QClaims) : ICommandHandler<Update_Org_Dto>
{
    async Task<Response<long?>> IRequestHandler<CommandRequest<Update_Org_Dto>, Response<long?>>.Handle(CommandRequest<Update_Org_Dto> request, CancellationToken cancellationToken)
    {
        var hasException = await Repository.AnyAsync(Specifications.Specs.OrganizationSpecs.CheckNameAlreadyExists(request.Dto.Id, request.Dto.OrgName!), cancellationToken, false, true);
        if (hasException.Status is Status.Exception)
            return hasException.Exception!;

        var OrgModelResult = await Repository.FirstOrDefaultAsync(Specifications.Specs.Common.GetById<Organization, long>(request.Dto.Id), cancellationToken);
        if (OrgModelResult.Status is Status.Exception)
            return OrgModelResult.Exception!;

        OrgModelResult.Value!.OrgName = request.Dto.OrgName;
        OrgModelResult.Value.OrgDetail = request.Dto.OrgDetail;
        OrgModelResult.Value.Active = request.Dto.Active;
        OrgModelResult.Value.ModifiedDate = DateTime.UtcNow;

        var RepositoryUpdateResult = await Repository.UpdateAsync(OrgModelResult.Value, cancellationToken, true);

        if (RepositoryUpdateResult.Status == Status.Exception)
            return RepositoryUpdateResult.Exception!;
        return OrgModelResult.Value.Id;
    }
}
