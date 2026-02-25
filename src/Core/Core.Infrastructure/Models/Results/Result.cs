using System.Net;

namespace Core.Infrastructure.Models.Results;

public sealed class Result
{
    /// <summary>
    /// Indicates whether the operation was successful. If false, the ErrorMessages property will contain details about the failure.
    /// </summary>
    public bool IsSuccess { get; set;}

    /// <summary>
    /// A list of error messages describing why the operation failed. This will be empty if IsSuccess is true.
    /// </summary>
    public List<string> ErrorMessages { get; set; } = [];

    /// <summary>
    /// The HTTP status code associated with the result. This can be used to provide more context about the outcome of the operation, especially in web applications.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
}

/// <summary>
/// Represents the result of an operation, including success status, error messages, and an optional data payload.
/// </summary> <typeparam name="T">The type of the data payload.</typeparam>
public sealed class Result<T>
{
    /// <summary>
    /// Indicates whether the operation was successful. If false, the ErrorMessages property will contain details about the failure.
    /// </summary>
    public bool IsSuccess { get; set;}

    /// <summary>
    /// A list of error messages describing why the operation failed. This will be empty if IsSuccess is true.
    /// </summary>
    public List<string> ErrorMessages { get; set; } = [];

    /// <summary>
    /// The HTTP status code associated with the result. This can be used to provide more context about the outcome of the operation, especially in web applications.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    /// <summary>
    /// The data payload of the result. This will be default(T) if IsSuccess is false or if no data is provided.
    /// </summary>
    public T? Data { get; set;} = default;
}