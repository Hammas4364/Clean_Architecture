using FluentValidation;

namespace Presentation.Validations.Common;

public static class ValidationExtensionMethods
{
    public static IRuleBuilderOptions<T, TElement> ShouldNotStartWithNumber<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new ShouldNotStartWithNumber<T, TElement>());
    }
}
