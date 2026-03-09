using System.Net;
using ApnaDhobi.Core.Infrastructure.Models;
namespace ApnaDhobi.Core.Infrastructure.Extensions;

public static class ResultExtensions
{
    public static Result<T> ToSuccesResult<T>(this T value, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new Result<T> { IsSuccess = true, Data = value, StatusCode = statusCode };
    }

    public static Result ToSuccesResult(this object value, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new Result { IsSuccess = true, StatusCode = statusCode };
    }

    public static Result ToFailureResult(this string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new Result { IsSuccess = false, ErrorMessages = [errorMessage], StatusCode = statusCode };
    }

    public static Result<T> ToFailureResult<T>(this string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new Result<T> { IsSuccess = false, ErrorMessages = [errorMessage], StatusCode = statusCode };
    }
}