namespace MpApi.Core.Exceptions;

/// <summary>
/// Base exception for all Materials Project API errors.
/// </summary>
public class MpApiException(string message, int? statusCode = null, string? responseBody = null, Exception? innerException = null) : Exception(message, innerException)
{
    public int? StatusCode { get; } = statusCode;
    public string? ResponseBody { get; } = responseBody;
}
