namespace MpApi.Core.Options;

/// <summary>
/// Configuration options for the Materials Project API Client.
/// </summary>
public sealed class MpApiOptions
{
    public const string DefaultBaseUrl = "https://api.materialsproject.org/";
    public const string DefaultUserAgent = "MpApi.NET/1.0.0 (.NET 10; OpenSource Client)";

    /// <summary>
    /// The Materials Project API key.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// The base URL of the Materials Project API. Defaults to https://api.materialsproject.org/
    /// </summary>
    public Uri BaseUrl { get; set; } = new(DefaultBaseUrl);

    /// <summary>
    /// User Agent header value to identify client requests.
    /// </summary>
    public string UserAgent { get; set; } = DefaultUserAgent;

    /// <summary>
    /// Request timeout duration. Defaults to 60 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60);

    /// <summary>
    /// Maximum number of automatic retries on transient errors or rate limits.
    /// </summary>
    public int MaxRetryCount { get; set; } = 3;
}