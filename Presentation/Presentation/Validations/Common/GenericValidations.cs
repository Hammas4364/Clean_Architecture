using FluentValidation;
using SharedKernel.Helpers;
namespace Presentation.Validations.Common;

public class GenericValidations : AbstractValidator<GetAllParams>
{
    public GenericValidations()
    {
        RuleFor(_ => _.SearchValue).MinimumLength(3).ShouldNotStartWithNumber();
    }
}

public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
{
    public BaseValidator()
    {
    }
}