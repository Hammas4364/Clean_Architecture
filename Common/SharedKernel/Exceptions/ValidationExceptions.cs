using AutoWrapper.Wrappers;
namespace SharedKernel.Exceptions;

public static class ValidationExceptions
{
    public static ApiException? GetValidationException(string message)
    {
        return default;
    }

    public static List<ValidationError> GetValidationErrors(string template, params string[] propertyNames)
    {
        var validationErrors = new List<ValidationError>();
        foreach (var property in propertyNames)
        {
            validationErrors.Add(new(property, property + template));
        }
        return validationErrors;
    }
}

