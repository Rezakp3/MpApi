using System.Text.Json.Serialization;

namespace MpApi.Common.Models;

/// <summary>
/// Generic envelope returned by Materials Project API endpoints.
/// </summary>
/// <typeparam name="T">The type of data payload.</typeparam>
public record MpResponse<T>
{
    /// <summary>
    /// Gets the payload data returned by the API.
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; init; }

    /// <summary>
    /// Gets the metadata associated with the response (e.g. pagination, total counts).
    /// </summary>
    [JsonPropertyName("meta")]
    public MpResponseMeta? Meta { get; init; }

    /// <summary>
    /// Gets the API schema version.
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get; init; }
}

/// <summary>
/// Metadata information for paginated or querying responses.
/// </summary>
public record MpResponseMeta
{
    [JsonPropertyName("total_doc")]
    public int? TotalDoc { get; init; }

    [JsonPropertyName("total_pages")]
    public int? TotalPages { get; init; }

    [JsonPropertyName("page")]
    public int? Page { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("time_taken")]
    public double? TimeTaken { get; init; }
}