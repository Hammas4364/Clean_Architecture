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
        var controller = await Repository.FirstOrDefaultAsync(
            Specs.Common.GetById<Organization, long>(request.Dto.Id), cancellationToken: cancellationToken);
        if (controller.Status is Status.Exception)
            return controller.Exception!;

        Response<Organization?>? reponse = await Repository.DeleteAsync(controller!.Value!.Delete(), cancellationToken: cancellationToken);

        return reponse.Status is Status.Exception ? reponse.Exception! : reponse.Value!.Id;
    }
}
