using System.Text.Json.Serialization;

namespace ProductManager.Domain.Common;

public record Result
{
    [JsonPropertyOrder(0)]
    public bool IsSuccess { get; init; }

    [JsonPropertyOrder(1)]
    public bool IsFailure => !IsSuccess;

    [JsonPropertyOrder(2)]
    public string? Error { get; init; }

    [JsonPropertyOrder(3)]
    public string? Message { get; init; }

    public static Result Success(string? message = null) =>
        new()
        {
            IsSuccess = true,
            Message = message ?? "Operation completed successfully."
        };

    public static Result Failure(string error, string? message = null) =>
        new()
        {
            IsSuccess = false,
            Error = error,
            Message = message ?? "Operation failed."
        };
}

public record Result<T> : Result
{
    [JsonPropertyOrder(4)]
    public T? Data { get; init; }

    public static Result<T> Success(T value, string? message = null) =>
        new()
        {
            IsSuccess = true,
            Data = value,
            Message = message ?? "Operation completed successfully."
        };

    public static new Result<T> Failure(string error, string? message = null) =>
        new()
        {
            IsSuccess = false,
            Error = error,
            Message = message ?? "Operation failed."
        };
}
