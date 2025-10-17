namespace Rasta.ProjectManagment.Application.Common.Models;

public class Result<T>
{
    internal Result(bool succeeded, string? message, IEnumerable<string>? errors, T? data)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Message = message;
        Data = data;
    }
    public T? Data { get; set; }
    public bool Succeeded { get; init; }
    public string? Message { get; init; }

    public string[]? Errors { get; init; }

    public static Result<T> Success(string message, T? data)
    {
        return new Result<T>(true, message, Array.Empty<string>(), data);
    }

    public static Result<T> Failure(string error, IEnumerable<string>? errors, T? data)
    {
        return new Result<T>(false, error, errors, data);
    }
}
