using System.Net;

namespace MpApi.Common.Exceptions;

/// <summary>
/// Base exception for all Materials Project API client errors.
/// </summary>
public class MpApiException : Exception
{
    public MpApiException(string message) : base(message) { }
    public MpApiException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when an HTTP error response is received from the Materials Project API.
/// </summary>
public class MpApiHttpException : MpApiException
{
    /// <summary>
    /// Gets the HTTP status code returned by the server.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the raw response payload if available.
    /// </summary>
    public string? ResponseBody { get; }

    public MpApiHttpException(HttpStatusCode statusCode, string message, string? responseBody = null)
        : base(message)
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }
}

/// <summary>
/// Exception thrown when authentication fails (HTTP 401 or 403).
/// </summary>
public sealed class MpApiAuthenticationException : MpApiHttpException
{
    public MpApiAuthenticationException(HttpStatusCode statusCode, string message, string? responseBody = null)
        : base(statusCode, message, responseBody) { }
}

/// <summary>
/// Exception thrown when request rate limits are exceeded (HTTP 429).
/// </summary>
public sealed class MpApiRateLimitException : MpApiHttpException
{
    /// <summary>
    /// Suggested wait time before making subsequent requests, if provided by the server.
    /// </summary>
    public TimeSpan? RetryAfter { get; }

    public MpApiRateLimitException(string message, TimeSpan? retryAfter = null, string? responseBody = null)
        : base(HttpStatusCode.TooManyRequests, message, responseBody)
    {
        RetryAfter = retryAfter;
    }
}

/// <summary>
/// Exception thrown when a requested resource or material identifier is not found (HTTP 404).
/// </summary>
public sealed class MpApiNotFoundException : MpApiHttpException
{
    public MpApiNotFoundException(string message, string? responseBody = null)
        : base(HttpStatusCode.NotFound, message, responseBody) { }
}