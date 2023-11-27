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
        var hasException = await Repository.AnyAsync(
           Specifications.Specs.OrgSpecs.CheckNameAlreadyExists(request.Dto.Id, request.Dto.OrgName),
            cancellationToken, false, true);

        if (hasException.Status is Status.Exception)
            return hasException.Exception!;

        var OrgModelResult = await Repository.FirstOrDefaultAsync(Specifications.Specs.Common.GetById<Organization, long>(request.Dto.Id), cancellationToken);

        if (OrgModelResult.Status is Status.Exception)
            return OrgModelResult.Exception!;

        var org = OrgModelResult.Value!;
        await Repository.EnableChangeTracker(org);

        org.Update(request.Dto);

        var qRepositoryAddResult = await Repository.SaveChangesAsync(cancellationToken);

        if (qRepositoryAddResult.Status is Status.Exception)
            return qRepositoryAddResult.Exception!;

        return await Task.FromResult(org.Id);
    }
}
