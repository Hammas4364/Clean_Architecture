using Domain.ViewModels;
using FluentValidation;

namespace Presentation.Validations;

public class OrgValidator
{
}

public class AddOrgValidator : AbstractValidator<Add_Org_Dto>
{
    public AddOrgValidator()
    {
        RuleFor(_ => _.OrgName).NotEmpty().MaximumLength(64);
        RuleFor(_ => _.OrgDetail).NotEmpty().NotNull().MaximumLength(200);
        RuleFor(_ => _.Active).NotNull();
    }
}

public class UpdateOrgValidator : AbstractValidator<Update_Org_Dto>
{
    public UpdateOrgValidator()
    {
        RuleFor(_ => _.Id).NotEmpty();
        RuleFor(_ => _.OrgName).NotEmpty().MaximumLength(64);
        RuleFor(_ => _.OrgDetail).NotEmpty().NotNull().MaximumLength(200);
    }
}

public class RemoveOrgValidator : AbstractValidator<Delete_Org_Dto>
{
    public RemoveOrgValidator()
    {
        RuleFor(_ => _.Id).NotEmpty();
    }
}