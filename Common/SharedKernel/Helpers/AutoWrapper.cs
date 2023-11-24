using AutoWrapper;
using AutoWrapper.Wrappers;
using Serilog;
using SharedKernel.Exceptions;
using System.Text;
namespace SharedKernel.Helpers;

public class MapResponseObject
{
    [AutoWrapperPropertyMap(Prop.Result)]
    public object? Payload { get; set; }


    [AutoWrapperPropertyMap(Prop.ResponseException)]
    public object? ErrorResponse { get; set; }

    [AutoWrapperPropertyMap(Prop.ResponseException_ExceptionMessage)]
    public string? Message { get; set; }

    [AutoWrapperPropertyMap(Prop.ResponseException_Details)]
    public string? Trace { get; set; }
}
public class AutoWrapper
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public object? Payload { get; set; }
    public DateTime SentDate { get; set; }
    public Pagination? Pagination { get; set; }

    public AutoWrapper(DateTime sentDate,
                               object? payload = null,
                               string message = "",
                               int statusCode = 200,
                               Pagination? pagination = null)
    {
        Code = statusCode;
        Message = message == string.Empty ? "Success" : message;
        Payload = payload;
        SentDate = sentDate;
        Pagination = pagination;
    }

    public AutoWrapper(DateTime sentDate,
                               object? payload = null,
                               Pagination? pagination = null)
    {
        Code = 200;
        Message = "Success";
        Payload = payload;
        SentDate = sentDate;
        Pagination = pagination;
    }

    public AutoWrapper(object? payload)
    {
        Code = 200;
        Payload = payload;
        Message = default;
    }

}
public class Pagination
{
    public int TotalItemsCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
public class responseException
{
    public string message { get; set; }    //Exception.Message
    public string trace { get; set; }     //Stack trace for error
    public string error_display { get; set; }     // Userfriendly Error for Display
    public responseException(string exceptionMessage, string stackTrace, string message)
    {
        this.message = message;
        trace = stackTrace;
        error_display = exceptionMessage;
    }
}
public static class AutoWrapperHelper
{
    public static ApiException GenerateError(Exception ex, string? message = default)
    {
        var validationException = ValidationExceptions.GetValidationException(ex.Message);
        if (validationException is not null)
            return validationException;
        if (ex is AppException qException)
        {
            if (qException.CustomError is not null)
            {
                Log.Error("Message: {Message}\nCustomError: {CustomError}\nStackTrace: {StackTrace}", qException.Message, qException.CustomError, qException.StackTrace);
                return new ApiException(qException.CustomError);
            }
            else if (qException.Errors is not null && qException.Errors.Any())
            {
                var sb = new StringBuilder();
                sb.AppendLine("[");
                foreach (var _ in qException.Errors)
                {
                    sb.AppendLine($"Name:\"{_.Name}\",");
                    sb.AppendLine($"Reason:\"{_.Reason}\",");
                    sb.AppendLine();
                }
                sb.AppendLine("]");
                Log.Error("Message: {Message}\nValidationErrors: {sb}", "Request responded with one or more validation errors.", sb);
                return new ApiException(qException.Errors);
            }

            Log.Error("Message: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            return new ApiException(ex);
        }
        else
        {
            Log.Error("Message: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            return new ApiException(ex);
        }
    }
}