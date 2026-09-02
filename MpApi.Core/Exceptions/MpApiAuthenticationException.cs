namespace MpApi.Core.Exceptions;

/// <summary>
/// Thrown when authentication fails (HTTP 401 / 403) due to an invalid or missing API key.
/// </summary>
public sealed class MpApiAuthenticationException
    (string message, int statusCode, string? responseBody = null)
    : MpApiException(message, statusCode, responseBody)
{
}