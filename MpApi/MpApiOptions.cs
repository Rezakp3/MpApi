namespace MpApi;

/// <summary>
/// Configuration options for the Materials Project API client.
/// </summary>
public sealed class MpApiOptions
{
    /// <summary>
    /// Default base URL for the Materials Project REST API.
    /// </summary>
    public const string DefaultBaseUrl = "https://api.materialsproject.org";

    /// <summary>
    /// Gets or sets the API key used for authentication.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the base URL of the Materials Project API.
    /// </summary>
    public Uri BaseUrl { get; set; } = new(DefaultBaseUrl);

    /// <summary>
    /// Gets or sets the HTTP request timeout. Default is 30 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Gets or sets the maximum number of retry attempts on transient HTTP failures or rate limits (429).
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;

    /// <summary>
    /// Validates the current options instance.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when required options like ApiKey are missing or invalid.</exception>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            throw new ArgumentException("API key cannot be null, empty, or whitespace. Please provide a valid Materials Project API key.", nameof(ApiKey));
        }

        if (BaseUrl is null || !BaseUrl.IsAbsoluteUri)
        {
            throw new ArgumentException("Base URL must be a valid absolute URI.", nameof(BaseUrl));
        }
    }
}