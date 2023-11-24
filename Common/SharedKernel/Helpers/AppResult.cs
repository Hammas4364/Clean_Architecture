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

public record AppResult
{
    protected AppResult() { }

    public AppResult(object? value)
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

    public AppResult(AppResult AppResult)
    {
        Value = AppResult.Value;
        Result = AppResult.Result;
        Exception = AppResult.Exception;
        Status = AppResult.Status;
    }

    public AppResult(Exception exception)
    {
        while (exception.InnerException is not null)
            exception = exception.InnerException;
        Exception = AutoWrapperHelper.GenerateError(exception);
        Value = default;
        Status = Status.Exception;
        Result = default!;
    }

    public AppResult(string exceptionMessage, int statusCode = 400)
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

public record AppResult<T> : AppResult
{
    public AppResult(T? value, string? notFoundMessage = default, string? okMessage = default)
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

    public AppResult(AppResult AppResult) : base(AppResult) { }

    public AppResult(Exception exception) : base(exception) { }

    public AppResult(string exceptionMessage, int statusCode = 400) : base(exceptionMessage, statusCode) { }

    public static implicit operator AppResult<T?>(T t)
    {
        return AppResults.From(t);
    }

    public static implicit operator AppResult<T?>(Exception e)
    {
        return AppResults.Exception<T>(e);
    }

    public new T? Value { get; set; }
}

public record AppResults
{
    public static AppResult<T> OK<T>(T value, string? okMessage = default)
    {
        return new AppResult<T>(value, default, okMessage);
    }
    public static AppResult<T?> NotFound<T>(string? notFoundMessage = default)
    {
        return new AppResult<T?>(default, notFoundMessage, default);
    }
    public static AppResult<T?> Exception<T>(Exception exception)
    {
        return new AppResult<T?>(exception: exception);
    }
    public static AppResult<T?> Exception<T>(string exceptionmessage, int statusCode = 400)
    {
        return new AppResult<T?>(exceptionmessage, statusCode);
    }
    public static AppResult<T?> From<T>(T? value, string? notFoundMessage = default, string? okMessage = default)
    {
        return new AppResult<T?>(value, notFoundMessage, okMessage);
    }
    public static AppResult<IEnumerable<T>?> From<T>(IEnumerable<T>? value, string? notFoundMessage = default, string? okMessage = default)
    {
        return new AppResult<IEnumerable<T>?>(value, notFoundMessage, okMessage);
    }
    public static AppResult<T?> InfoMessage<T>(T value, string? InfoMessage = default, string? okMessage = default)
    {
        return new AppResult<T?>(value, InfoMessage, okMessage);
    }
    public static AppResult<T?> From<T>(AppResult AppResult)
    {
        return new AppResult<T?>(AppResult);
    }
}
