namespace MpApi.Core.Exceptions;

/// <summary>
/// Thrown when the API returns an HTTP 429 Too Many Requests.
/// </summary>
public sealed class MpApiRateLimitException
    (string message, TimeSpan? retryAfter, string? responseBody = null) 
    : MpApiException(message, 429, responseBody)
{
    public TimeSpan? RetryAfter { get; } = retryAfter;
}
