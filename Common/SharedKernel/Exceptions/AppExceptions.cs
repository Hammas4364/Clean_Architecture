using AutoWrapper.Wrappers;
namespace SharedKernel.Exceptions;

public class AppException : ApiException
{
    public AppException(IEnumerable<ValidationError> errors, int statusCode = 400) : base(errors, statusCode)
    {
    }

    public AppException(object customError, int statusCode = 400) : base(customError, statusCode)
    {
    }

    public AppException(Exception ex, int statusCode = 500) : base(ex, statusCode)
    {
    }

    public AppException(string message, int statusCode = 400, string? errorCode = null, string? refLink = null) : base(message, statusCode, errorCode, refLink)
    {
    }
}
