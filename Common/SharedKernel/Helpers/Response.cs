using AutoWrapper.Wrappers;
using Serilog;
using SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Helpers;

public enum Status
{
    Ok,
    NotFound,
    Exception
}

public record Response
{
    protected Response() { }

    public Response(object? value)
    {
        Value = value;
        if (value is null)
        {
            Result = new ApiResponse(value?.GetType().Name + ".NotFound", null, 200);
            Status = Status.NotFound;
            Log.Warning("Message:{Message}", Result.Message);
        }
        else
        {
            Result = new(value, 200);
            Status = Status.Ok;
        }
    }

    public Response(Response AppResult)
    {
        Value = AppResult.Value;
        Result = AppResult.Result;
        Exception = AppResult.Exception;
        Status = AppResult.Status;
    }

    public Response(Exception exception)
    {
        while (exception.InnerException is not null)
            exception = exception.InnerException;
        Exception = AutoWrapperHelper.GenerateError(exception);
        Value = default;
        Status = Status.Exception;
        Result = default!;
    }

    public Response(string exceptionMessage, int statusCode = 400)
    {
        Exception = AutoWrapperHelper.GenerateError(new AppException(exceptionMessage, statusCode));
        Value = default;
        Status = Status.Exception;
        Result = default!;
        Log.Error("{exceptionMessage}", exceptionMessage);
    }

    public object? Value { get; set; }
    public ApiResponse? Result { get; set; }
    public Status Status { get; set; }
    public Exception? Exception { get; set; }
}

public record Response<T> : Response
{
    public Response(T? value, string? notFoundMessage = default, string? okMessage = default)
    {
        Value = value;

        if (value is null)
        {
            Result = new ApiResponse(notFoundMessage ?? $"{typeof(T).Name}.NotFound", null, 200);
            Exception = new ApiException(notFoundMessage ?? $"{typeof(T).Name}.NotFound");
            Status = Status.NotFound;
            Log.Warning("Message:{Message}", Result.Message);
        }
        else
        {
            Result = okMessage is null ? new(value, 200) : new(okMessage, value, 200);
            Status = Status.Ok;
        }
    }

    public Response(Response AppResult) : base(AppResult) { }

    public Response(Exception exception) : base(exception) { }

    public Response(string exceptionMessage, int statusCode = 400) : base(exceptionMessage, statusCode) { }

    public static implicit operator Response<T?>(T t)
    {
        return ResponseResult.From(t);
    }

    public static implicit operator Response<T?>(Exception e)
    {
        return ResponseResult.Exception<T>(e);
    }

    public new T? Value { get; set; }
}

public record ResponseResult
{
    public static Response<T> OK<T>(T value, string? okMessage = default)
    {
        return new Response<T>(value, default, okMessage);
    }
    public static Response<T?> NotFound<T>(string? notFoundMessage = default)
    {
        return new Response<T?>(default, notFoundMessage, default);
    }
    public static Response<T?> Exception<T>(Exception exception)
    {
        return new Response<T?>(exception: exception);
    }
    public static Response<T?> Exception<T>(string exceptionmessage, int statusCode = 400)
    {
        return new Response<T?>(exceptionmessage, statusCode);
    }
    public static Response<T?> From<T>(T? value, string? notFoundMessage = default, string? okMessage = default)
    {
        return new Response<T?>(value, notFoundMessage, okMessage);
    }
    public static Response<IEnumerable<T>?> From<T>(IEnumerable<T>? value, string? notFoundMessage = default, string? okMessage = default)
    {
        return new Response<IEnumerable<T>?>(value, notFoundMessage, okMessage);
    }
    public static Response<T?> InfoMessage<T>(T value, string? InfoMessage = default, string? okMessage = default)
    {
        return new Response<T?>(value, InfoMessage, okMessage);
    }
    public static Response<T?> From<T>(Response AppResult)
    {
        return new Response<T?>(AppResult);
    }
}
